using Microsoft.Extensions.DependencyInjection;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Request;
using qodeless.messaging.Worker.ViewModels.Response;
using qodeless.services.api.SysCobrancaClient.Managers.Zenvia;
using System.Net.Sockets;

namespace qodeless.messaging.Worker.Commands
{
    public class CreditPlayerCommand : BaseCommand<CreditPlayerRequest, CreditPlayerResponse>
    {

        public override CreditPlayerResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null)
            return new CreditPlayerResponse()
            {
                Status = ECommandStatus.NAK
            };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var sitePlayerAppService = scope.ServiceProvider.GetRequiredService<ISitePlayerAppService>();
                var result = sitePlayerAppService.GetBySiteIdPlayerId(request.SiteId, request.PlayerId);

                if (result != null)
                {
                    var creditAmount = result.CreditAmount;
                    if (request.NewCreditValue != 0)
                    {
                        creditAmount = request.NewCreditValue;
                        result.CreditAmount = creditAmount;
                        if (!sitePlayerAppService.Upsert(result).IsValid)
                        {
                            return new CreditPlayerResponse()
                            {
                                Status = ECommandStatus.NAK
                            };
                        }
                    }

                    return new CreditPlayerResponse()
                    {
                        AmountCredit = creditAmount,
                        Status = ECommandStatus.ACK
                    };
                }
                
                return new CreditPlayerResponse()
                {
                    Status = ECommandStatus.NAK
                };
            }
            #endregion //BUSINESS_LOGIC_LAYER_HERE

            return new CreditPlayerResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}
