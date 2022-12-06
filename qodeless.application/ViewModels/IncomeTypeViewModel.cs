using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class IncomeTypeViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
