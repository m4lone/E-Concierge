using qodeless.messaging.Worker.ViewModels.Common;
using System;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class DeviceInResponse : BaseResponse
    {
        public Guid SessionDeviceId { get; set; }
        public bool Success { get; set; }
    }
}
