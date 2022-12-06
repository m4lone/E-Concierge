using qodeless.messaging.Worker.ViewModels.Common;
using System;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class PixPaidConfirmationResponse : BaseResponse
    {
        public bool Paid { get; set; }
    }
}
