using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.ViewModels
{
    public class InviteResponseViewModel
    {
        public ValidationResult ValidationResult { get; set; }
        public string Token { get; set; }

        public InviteResponseViewModel(ValidationResult validationResult, string token)
        {
            ValidationResult = validationResult;
            Token = token;
        }
    }
}
