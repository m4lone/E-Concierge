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
using Qodeless.Domain.Enum;

namespace qodeless.messaging.Worker.Commands
{
    public class PaymentCommand : BaseCommand<PaymentRequest, PaymentResponse>
    {
        private PixManager _pixManager { get; set; } = new PixManager();
        public override PaymentResponse Run(TcpClient client, byte[] data)
        {

            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new PaymentResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var expireAt = DateTime.Now.AddDays(1);
                var orderAppService = scope.ServiceProvider.GetRequiredService<IOrderServices>();
                var siteAppService = scope.ServiceProvider.GetRequiredService<ISiteAppService>();

                var token = _pixManager.GetPixAuth("7", "43QESGXSHDi1eAY5L1cd4eOaT8eIH8BpG39UgydM").AccessToken;
                var result = _pixManager.GetPix(token, request.Amount);
                var site = siteAppService.GetById(request.SiteId);
                var resultOrder = orderAppService.Add(new OrderViewModel() 
                {
                    Code = orderAppService.GenerateRandomNumber().Result.Value.ToString(),
                    EOrderStatus = EOrderStatus.Unpaid,
                    PixId = result.PixId,
                    QrCode = result.Qrcode,
                    UserId = request.UserPlayId,
                    Date = result.ExpirationDate,
                    SiteId = request.SiteId,
                    Value = request.Amount,
                    Fee = 0,
                    AccountId = site.AccountId
                });
                if (resultOrder)
                {
                    return new PaymentResponse()
                    {
                        Status = ECommandStatus.ACK,
                        QrCode = result.Qrcode,
                        Base64 = result.Base64,
                        PixId = result.PixId
                    };
                }
            }
            #endregion

            return new PaymentResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}