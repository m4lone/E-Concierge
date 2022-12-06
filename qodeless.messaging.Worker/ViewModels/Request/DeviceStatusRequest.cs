using System;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class DeviceStatusRequest
    {
        public DeviceStatusRequest(Guid deviceId, string aspNetUserId)
        {
            DeviceId = deviceId;
            AspNetUserId = aspNetUserId;
        }

        public Guid DeviceId { get; set; }
        public string AspNetUserId { get; set; }
    }
}
