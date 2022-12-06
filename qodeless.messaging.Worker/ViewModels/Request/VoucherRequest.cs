using System;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class VoucherRequest
    {
        public string Code { get; set; }
        public Guid SiteId { get; set; }
        public string UserId { get; set; }
    }
}
