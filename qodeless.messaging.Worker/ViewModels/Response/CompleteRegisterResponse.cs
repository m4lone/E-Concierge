using qodeless.messaging.Worker.ViewModels.Common;
using System;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class CompleteRegisterResponse : BaseResponse
    {
        public string AspNetUserId { get; set; }
    }
}
