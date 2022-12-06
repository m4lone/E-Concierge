using qodeless.messaging.Worker.ViewModels.Common;
using System;

namespace qodeless.messaging.Worker.ViewModels.Response
{
    public class JoinResponse : BaseResponse
    {
        public string AspNetUserId { get; set; }
        public Guid SiteId { get; set; }
    }
}
