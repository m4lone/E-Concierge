using Microsoft.Extensions.DependencyInjection;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Response;
using qodeless.messaging.Worker.ViewModels.Request;
using System.Net.Sockets;
using qodeless.application;
using System.Linq;
using qodeless.application.Enums;
using System;
using qodeless.domain.Extensions;

namespace qodeless.messaging.Worker.Commands
{
    public class DeviceStatusCommand : BaseCommand<DeviceStatusRequest, DeviceStatusResponse>
    {
        // public EDeviceAvailability Availability { get; set; } NÃO TEM NA API

        public override DeviceStatusResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new DeviceStatusResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var sessionDeviceAppService = scope.ServiceProvider.GetRequiredService<ISessionDeviceAppService>();
                var state = EDeviceState.Busy;               

                var result = sessionDeviceAppService
                    .GetAllBy(c=> c.DeviceId == request.DeviceId 
                        && c.DtEnd == DateTime.UnixEpoch 
                        && c.DtBegin > DateTime.Now.AddDays(-3).ToFirstMoment() 
                        && c.UserPlayId != request.AspNetUserId
                    ).FirstOrDefault();



                if (result == null)
                {
                    state = EDeviceState.Available;
                }

                return new DeviceStatusResponse()
                {
                    Status = ECommandStatus.ACK,
                    State = state,
                    BusyNickName = result == null ? string.Empty : sessionDeviceAppService.GetUserById(result.UserPlayId).NickName
                };
            }
            
            #endregion //BUSINESS_LOGIC_LAYER_HERE

            return new DeviceStatusResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}
