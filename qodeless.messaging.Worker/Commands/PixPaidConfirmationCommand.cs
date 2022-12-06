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
    public class PixPaidConfirmationCommand : BaseCommand<PixPaidConfirmationRequest, PixPaidConfirmationResponse>
    {
        private PixManager _pixManager { get; set; } = new PixManager();
        public override PixPaidConfirmationResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new PixPaidConfirmationResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var PaymentAppService = scope.ServiceProvider.GetRequiredService<IOrderServices>();

                var token = _pixManager.GetPixAuth("7", "43QESGXSHDi1eAY5L1cd4eOaT8eIH8BpG39UgydM").AccessToken;
                var result = _pixManager.GetPixStatus(token, request.PixId);

                return new PixPaidConfirmationResponse()
                {
                    Status = ECommandStatus.ACK,
                    Paid = result.Paid
                };
            }
            #endregion

            return new PixPaidConfirmationResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}