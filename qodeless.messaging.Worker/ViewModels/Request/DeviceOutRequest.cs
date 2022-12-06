using System;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class DeviceOutRequest
    {
        public DeviceOutRequest(Guid sessionDeviceId, Guid deviceId, string aspNetUserId)
        {
            SessionDeviceId = sessionDeviceId;
            DeviceId = deviceId;
            AspNetUserId = aspNetUserId;
        }

        public Guid SessionDeviceId { get; set; }
        public Guid DeviceId { get; set; }
        public string AspNetUserId { get; set; }
    }
}
