using Microsoft.Extensions.DependencyInjection;
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
    public class TokenCommand : BaseCommand<TokenRequest, TokenResponse>
    {

        public override TokenResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null)
                return new TokenResponse()
                {
                    Status = ECommandStatus.NAK
                };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var tokenAppService = scope.ServiceProvider.GetRequiredService<ITokenAppService>();

                var token = new TokenViewModel(request.Phone);
                var result = tokenAppService.Add(token).Result;
                if (result.IsValid)
                {
                    WhatsappSenderManager.SendWhatsapp(request.Phone, token.Code);
                    if (SmsSenderManager.SendSms(request.Phone, token.Code))
                    {
                        return new TokenResponse()
                        {
                            Status = ECommandStatus.ACK,
                            TokenNumber = token.Code
                        };
                    }
                    else
                    {
                        return new TokenResponse()
                        {
                            Status = ECommandStatus.NAK
                        };
                    }
                }
            }
            #endregion //BUSINESS_LOGIC_LAYER_HERE

            return new TokenResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}
