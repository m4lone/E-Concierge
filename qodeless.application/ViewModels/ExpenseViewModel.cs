using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class ExpenseViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public EIncomeType Type { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? SiteId { get; set; }
        public string AccountName { get; set; }
        public string SiteName { get; set; }
        public DateTime DueDate { get; set; }
    }
}
