using Microsoft.Extensions.DependencyInjection;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Response;
using System.Net.Sockets;
using qodeless.application;
using System;
using qodeless.application.ViewModels;
using qodeless.messaging.Worker.ViewModels.Request;
using qodeless.application.Managers.Pix;

namespace qodeless.messaging.Worker.Commands
{
    public class GameCoinBetCommand : BaseCommand<GameCoinBetRequest, GameCoinBetResponse>
    {
        public override GameCoinBetResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new GameCoinBetResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var siteAppService = scope.ServiceProvider.GetRequiredService<ISiteAppService>();
                var result = siteAppService.GetCurrencyGame(request.SiteId);
                if(result != null)
                {
                    return new GameCoinBetResponse()
                    {
                        Status = ECommandStatus.ACK,
                        GameCoinBet = result
                    };
                }
            }
            #endregion
            return new GameCoinBetResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}