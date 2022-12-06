using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class CurrencyViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public ECurrencyCode Code { get; set; }
        public double VlrToBRL { get; set; }
    }
}
