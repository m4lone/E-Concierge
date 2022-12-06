using Microsoft.Extensions.DependencyInjection;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Response;
using System.Net.Sockets;
using qodeless.application;
using System;
using qodeless.application.ViewModels;
using qodeless.messaging.Worker.ViewModels.Request;

namespace qodeless.messaging.Worker.Commands
{
    public class DeviceInCommand : BaseCommand<DeviceInRequest, DeviceInResponse>
    {
        public override DeviceInResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new DeviceInResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var sessionDeviceAppService = scope.ServiceProvider.GetRequiredService<ISessionDeviceAppService>();
                var session = new SessionDeviceViewModel()
                {
                    Id = Guid.NewGuid(),
                    DeviceId = request.DeviceId,
                    DtBegin = DateTime.Now,
                    UserPlayId = request.AspNetUserId,
                    DtEnd = DateTime.UnixEpoch,
                    LastIpAddress = "255.255.255.255"
                };
                var result = sessionDeviceAppService.Add(session);

                return new DeviceInResponse()
                {
                    Status = ECommandStatus.ACK,
                    Success = result.IsValid,
                    SessionDeviceId = session.Id
                };
            }
            
            #endregion //BUSINESS_LOGIC_LAYER_HERE

            return new DeviceInResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}
