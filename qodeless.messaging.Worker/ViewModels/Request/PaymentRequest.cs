using System;
using qodeless.messaging.Worker.Enum;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class PaymentRequest
    {
        public Guid SiteId { get; set; }
        public string UserPlayId { get; set; }
        public long Amount { get; set; }
    }
}
