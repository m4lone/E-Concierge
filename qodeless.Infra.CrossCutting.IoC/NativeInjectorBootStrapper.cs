using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.Services;
using qodeless.domain.Interfaces;
using qodeless.domain.Interfaces.Repositories;
using qodeless.Infra.CrossCutting.Identity.Authorization;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.Infra.CrossCutting.Identity.Services;

namespace qodeless.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();

            // Data - Repository
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountGameRepository, AccountGameRepository>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IPlayRepository, PlayRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceGameRepository, DeviceGameRepository>();
            services.AddScoped<ISessionSiteRepository, SessionSiteRepository>();
            services.AddScoped<ISessionDeviceRepository, SessionDeviceRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameSettingRepository, GameSettingRepository>();
            services.AddScoped<ISuccessFeeRepository, SuccessFeeRepository>();
            services.AddScoped<IVouscherRepository, VouscherRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IIncomeTypeRepository, IncomeTypeRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>(); 
            services.AddScoped<IExpenseRequestRepository, ExpenseRequestRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ISiteGameRepository, SiteGameRepository>();
            services.AddScoped<ISitePlayerRepository, SitePlayerRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IVirtualBalanceRepository, VirtualBalanceRepository>();
            services.AddScoped<ISiteDeviceRepository, SiteDeviceRepository>();
            services.AddScoped<IInviteRepository, InviteRepository>();
            services.AddScoped<IExchangeRepository, ExchangeRepository>();
            services.AddScoped<ICardStonesRepository, CardStonesRepository>();

            // Application - Services
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IGroupAppService, GroupAppService>();
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IAccountGameAppService, AccountGameAppService>();
            services.AddScoped<ISiteAppService, SiteAppService>();
            services.AddScoped<IPlayAppService, PlayAppService>();
            services.AddScoped<IDeviceAppService, DeviceAppService>();
            services.AddScoped<IDeviceGameAppService, DeviceGameAppService>();
            services.AddScoped<IGameAppService, GameAppService>();
            services.AddScoped<IGameSettingAppService, GameSettingAppService>();
            services.AddScoped<ISessionSiteAppService, SessionSiteAppService>();
            services.AddScoped<ISessionDeviceAppService, SessionDeviceAppService>();
            services.AddScoped<ISuccessFeeAppService, SuccessFeeAppService>();
            services.AddScoped<IVouscherAppService, VouscherAppService>();
            services.AddScoped<ICurrencyAppService, CurrencyAppService>();
            services.AddScoped<IIncomeAppService, IncomeAppService>();
            services.AddScoped<IIncomeTypeAppService, IncomeTypeAppService>();
            services.AddScoped<IExpenseAppService, ExpenseAppService>();
            services.AddScoped<IExpenseRequestAppService, ExpenseRequestAppService>();
            services.AddScoped<ISiteGameAppService, SiteGameAppService>();
            services.AddScoped<ISitePlayerAppService, SitePlayerAppService>();
            services.AddScoped<ITokenAppService, TokenAppService>();
            services.AddScoped<IVirtualBalanceAppService, VirtualBalanceAppService>();
            services.AddScoped<ISiteDeviceAppService, SiteDeviceAppService>();
            services.AddScoped<IInviteAppService, InviteAppService>();
            services.AddScoped<IGeoAppService, GeoAppService>();
            services.AddScoped<IExchangeAppService, ExchangeAppService>();
            services.AddScoped<ICardStonesAppService, CardStonesAppService>();

            services.AddDbContext<ApplicationDbContext>(options => 
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddTransient<IAppDbContext, ApplicationDbContext>();
        }
    }
}
