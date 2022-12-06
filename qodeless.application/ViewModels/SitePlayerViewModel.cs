using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class SitePlayerViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public string UserPlayerId { get; set; }
        public string SiteName { get; internal set; }
        public string Email { get; set; }
        public int CreditAmount { get; set; }
    }
}
