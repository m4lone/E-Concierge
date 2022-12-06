using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class ExchangeViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Note { get; set; }
        public double Value { get; set; }
        public Guid AccountId { get; set; }
    }
}
