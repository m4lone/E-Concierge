using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using qodeless.application.AutoMapper;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.Infra.CrossCutting.IoC;
using qodeless.services.WebApi.Extension;
using qodeless.services.WebApi.Hubs;
using qodeless.WebApi.Quartz;
using Quartz;
using Quartz.Impl;
using System.Globalization;
using System.Text;

namespace qodeless.services.WebAPI
{
    public class Startup
    {
        public string WebRootPath { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebRootPath = env.WebRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Configure JWT
            #region JWT Configuration
            var jwtSection = Configuration.GetSection("JWTSetup");
            services.Configure<JWTSetup>(jwtSection);

            var jwtSetup = jwtSection.Get<JWTSetup>();
            var key = Encoding.ASCII.GetBytes(jwtSetup.Secret);

            #endregion

            services.Configure<URLSettings>(options =>
            {
                options.URL = Configuration.GetSection("UrlSetup:URL").Value;
            });

            //Exntension
            services.AddJwtAuthorization(WebRootPath);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSetup.ValidOn,
                    ValidIssuer = jwtSetup.Emiter
                };
            });

            //Configurando o serviço de documentação do Swagger
            #region Config Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RG Digital API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion

            services.AddCors(o => o.AddPolicy("CorsApi", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllersWithViews();
            // Configurando AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
            services.AddSignalR();

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            var cultureInfo = new CultureInfo("en"); //MM/dd/yyyy
            cultureInfo.NumberFormat.CurrencySymbol = "R$";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UBUNTU API");
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "UBUNTU API";
                c.InjectStylesheet("/docs/SwaggerHeader.css");
            });

            app.UseRouting();
            app.UseCors("CorsApi");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<QodelessHub>("/QodelessHub");
            });

        }
        private void RegisterServices(IServiceCollection services)
        {
            // Add Dependencies from another file.
            services.AddTransient<PixUpdateStatusJob>();
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
            services.AddHttpContextAccessor();

            //QUARTZ - STARTS
            var container = services.BuildServiceProvider();
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.JobFactory = new JobFactory(container);
            ScheduleJobs(scheduler);
            scheduler.Start();
            //QUARTZ - ENDS
        }
        /// <summary>
        /// QUARTZ IMPLEMENT
        /// </summary>
        private void ScheduleJobs(IScheduler scheduler)
        {
            const int TIMER_120_SECONDS = 120;
            IJobDetail job =
                JobBuilder.Create<PixUpdateStatusJob>()
                .WithIdentity("PixUpdateStatusJob", "pixGroup")
                .Build();


            // Trigger para que o job seja executado imediatamente, e repetidamente a cada 5 segundos
            ITrigger trigger5seconds =
                TriggerBuilder.Create().WithIdentity("pixTrigger", "pixGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(TIMER_120_SECONDS)
                        .RepeatForever())
                    .Build();

            scheduler.ScheduleJob(job, trigger5seconds).Wait();
        }
    }
}
