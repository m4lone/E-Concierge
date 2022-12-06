using qodeless.domain.Entities;
using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class VouscherViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string UserOperationId { get; set; }
        public string QrCodeKey { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }
        public Guid SiteID { get; set; }
        public bool IsActive { get; set; }
        public string SiteName { get; set; }
    }
}
