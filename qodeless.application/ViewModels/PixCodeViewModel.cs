using System;

namespace qodeless.application.ViewModels
{
    public class PixCodeViewModel
    {
        public long Fee { get; set; }
        public float Value { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UserId { get; set; }
        public Guid? SiteId { get; set; }
        public Guid? AccountId { get; set; }
    }
}
