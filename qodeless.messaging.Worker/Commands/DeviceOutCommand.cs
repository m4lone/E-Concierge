using Microsoft.Extensions.DependencyInjection;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Response;
using System.Net.Sockets;
using qodeless.application;
using System.Linq;
using System;
using qodeless.messaging.Worker.ViewModels.Request;

namespace qodeless.messaging.Worker.Commands
{
    public class DeviceOutCommand : BaseCommand<DeviceOutRequest, DeviceOutResponse>
    {
        public override DeviceOutResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new DeviceOutResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var sessionDeviceAppService = scope.ServiceProvider.GetRequiredService<ISessionDeviceAppService>();

                var entity = sessionDeviceAppService.GetAllBy(c=> c.Id == request.SessionDeviceId) .FirstOrDefault();
                entity.DtEnd = DateTime.Now;
                var result = sessionDeviceAppService.Update(entity);

                return new DeviceOutResponse()
                {
                    Status = ECommandStatus.ACK,
                    Success = result.IsValid
                };
            }

            #endregion //BUSINESS_LOGIC_LAYER_HERE

            return new DeviceOutResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}
