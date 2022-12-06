using AutoMapper;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AccountViewModel,Account>();
            CreateMap<AccountGameViewModel,AccountGame>();
            CreateMap<DeviceViewModel, Device>();
            CreateMap<DeviceGameViewModel, DeviceGame>();
            CreateMap<GroupViewModel, Group>();
            CreateMap<SiteViewModel,Site>();
            CreateMap<SiteGameViewModel,SiteGame>();
            CreateMap<SitePlayerViewModel,SitePlayer>();
            CreateMap<PlayViewModel,Play>();            
            CreateMap<GameViewModel, Game>();
            CreateMap<GameSettingViewModel, GameSetting>();
            CreateMap<SessionSiteViewModel,SessionSite>();
            CreateMap<SessionDeviceViewModel, SessionDevice>();
            CreateMap<SuccessFeeViewModel, SuccessFee>();
            CreateMap<VouscherViewModel, Voucher>();
            CreateMap<CurrencyViewModel, Currency>();
            CreateMap<IncomeViewModel, Income>();
            CreateMap<IncomeTypeViewModel, IncomeType>();
            CreateMap<ExpenseViewModel, Expense>();
            CreateMap<ExpenseRequestViewModel, ExpenseRequest>();
            CreateMap<TokenViewModel, Token>();
            CreateMap<VirtualBalanceViewModel, VirtualBalance>();
            CreateMap<SiteDeviceViewModel, SiteDevice>();
            CreateMap<RequestAuthViewModel, RequestAuth>();
            CreateMap<ResponseAuthViewModel, ResponseAuth>();
            CreateMap<InviteViewModel, Invite>();
            CreateMap<OrderViewModel, Order>();
            CreateMap<ExchangeViewModel, Exchange>();
            CreateMap<CardStonesViewModel, CardStones>();
        }
    }
}
