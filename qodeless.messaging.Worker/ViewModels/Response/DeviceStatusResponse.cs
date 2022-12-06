using qodeless.application.Enums;
using qodeless.messaging.Worker.ViewModels.Common;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class DeviceStatusResponse : BaseResponse
    {
        public EDeviceState State { get; set; }
        public string BusyNickName { get; set; }
    }
}
