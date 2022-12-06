using System;
using qodeless.messaging.Worker.Enum;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class PixPaidConfirmationRequest
    {
        public PixPaidConfirmationRequest(string userPlayId, string pixId)
        {
            UserPlayId = userPlayId;
            PixId = pixId;
        }

        public string UserPlayId { get; set; }
        public string PixId { get; set; }
    }
}
