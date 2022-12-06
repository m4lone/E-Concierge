using qodeless.application.Enums;
using qodeless.messaging.Worker.ViewModels.Common;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class ChargeResponse : BaseResponse
    {
        public double Amount { get; set; }
    }
}
