using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class SessionSiteViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string UserOperationId { get; set; }
        public DateTime DtBegin { get; set; }
        public DateTime DtEnd { get; set; }
        public Guid SiteId { get; set; }
        public EStatusSessionSite Status { get; set; }
    }
}
