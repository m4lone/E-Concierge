using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class SuccessFeeViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string UserId { get; set; }
        public double Rate { get; set; }
        public EFeeType Type { get; set; }
        public Guid ParentUserId { get; set; }
        public Guid ParentSiteId { get; set; }
        public Guid SiteId { get; set; }
    }
}
