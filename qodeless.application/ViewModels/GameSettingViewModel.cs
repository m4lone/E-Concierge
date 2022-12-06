using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class GameSettingViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public decimal ExtraBallValue { get; set; }
        public decimal BallValue { get; set; }
        public Guid GameId { get; set; }
    }
}





