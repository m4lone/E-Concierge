using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class VouscherValidationViewModel
    {        
        public string QrCodeKey { get; set; }
        public bool Expired { get; set; }
    }
}
