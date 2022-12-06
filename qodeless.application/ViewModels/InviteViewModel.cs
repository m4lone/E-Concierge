using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class InviteViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string InviteToken { get; set; }
        public Guid SiteId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DtExpiration { get; set; }
    }
}
