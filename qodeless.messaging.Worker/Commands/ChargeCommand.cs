using Microsoft.Extensions.DependencyInjection;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Response;
using qodeless.messaging.Worker.ViewModels.Request;
using System.Net.Sockets;
using qodeless.application;
using System.Linq;

namespace qodeless.messaging.Worker.Commands
{
    public class ChargeCommand : BaseCommand<ChargeRequest, ChargeResponse>
    {
        // public EDeviceAvailability Availability { get; set; } NÃO TEM NA API

        public override ChargeResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new ChargeResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                double amount;

                var VirtualBalanceAppService = scope.ServiceProvider.GetRequiredService<IVirtualBalanceAppService>();
               
                var result = VirtualBalanceAppService.GetAllBy(c => c.UserPlayId == request.UserPlayId).Select(c => c.Amount).ToList();
                amount = result[0];

                //VirtualBalanceAppService.GetAllBy(c => c.UserPlayId == request.UserPlayId).Select(c => c.Amount)
                //amount = result.

                return new ChargeResponse()
                {
                    Status = ECommandStatus.ACK,
                    Amount = amount
                };
            }
            
            #endregion //BUSINESS_LOGIC_LAYER_HERE

            return new ChargeResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}
