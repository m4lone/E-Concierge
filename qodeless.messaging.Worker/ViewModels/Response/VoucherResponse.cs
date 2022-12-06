using qodeless.messaging.Worker.ViewModels.Common;
using System;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class VoucherResponse : BaseResponse
    {
        public int CreditAmount { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
