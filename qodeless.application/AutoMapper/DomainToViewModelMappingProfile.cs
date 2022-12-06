using AutoMapper;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Group, GroupViewModel>();
            CreateMap<Account, AccountViewModel>();
            CreateMap<AccountGame, AccountGameViewModel>();
            CreateMap<Play, PlayViewModel>();
            CreateMap<Device, DeviceViewModel>();
            CreateMap<DeviceGame, DeviceGameViewModel>();
            CreateMap<Game, GameViewModel>();
            CreateMap<SessionDevice, SessionDeviceViewModel>();
            CreateMap<SessionSite, SessionSiteViewModel>();
            CreateMap<Site, SiteViewModel>();
            CreateMap<SiteGame, SiteGameViewModel>();
            CreateMap<SitePlayer, SitePlayerViewModel>();
            CreateMap<SuccessFee, SuccessFeeViewModel>();
            CreateMap<Voucher, VouscherViewModel>();
            CreateMap<Currency, CurrencyViewModel>();
            CreateMap<Income, IncomeViewModel>();
            CreateMap<IncomeType, IncomeTypeViewModel>();
            CreateMap<Expense, ExpenseViewModel>();
            CreateMap<ExpenseRequest, ExpenseRequestViewModel>();
            CreateMap<Token, TokenViewModel>();
            CreateMap<VirtualBalance, VirtualBalanceViewModel>();
            CreateMap<SiteDevice, SiteDeviceViewModel>();
            CreateMap<RequestAuth, RequestAuthViewModel>();
            CreateMap<ResponseAuth, ResponseAuthViewModel>();
            CreateMap<Invite, InviteViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<Exchange, ExchangeViewModel>();
            CreateMap<GameSetting, GameSettingViewModel>();
            CreateMap<CardStones, CardStonesViewModel>();
        }
    }
}
