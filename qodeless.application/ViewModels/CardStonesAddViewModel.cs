using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class CardStonesAddViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string StoneNumbersList { get; set; }
        public Guid GameId { get; set; }
    }  
}
