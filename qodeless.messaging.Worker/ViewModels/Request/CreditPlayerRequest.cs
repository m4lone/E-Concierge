using System;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class CreditPlayerRequest
    {
        public Guid SiteId { get; set; }
        public string PlayerId { get; set; }
        public int NewCreditValue { get; set; }
        public CreditPlayerRequest(Guid siteId, string playerId, int newCreditValue)
        {
            SiteId = siteId;
            PlayerId = playerId;
            NewCreditValue = newCreditValue;
        }
    }
}
