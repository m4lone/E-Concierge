using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using qodeless.application.AutoMapper;
using qodeless.application.Interfaces;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.Infra.CrossCutting.IoC;

namespace qodeless.messaging.Worker.Manager
{
    public sealed class ServiceManager
    {
        public ISiteGameAppService siteGameAppService;
        public ITokenAppService tokenAppService;
        public ServiceProvider serviceProvider;

        private void AddServices(IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>();
            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceProvider = services.BuildServiceProvider();
        }


        #region CORE_SERVICES
        private void InitApplicationServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
            NativeInjectorBootStrapper.RegisterServices(services);
            AddServices(services);          
        }
        private static ServiceManager instance = null;
        private static readonly object padlock = new object();
        ServiceManager() { }
        public static ServiceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceManager();
                            instance.InitApplicationServices();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion //CORE_SERVICES
    }

}
