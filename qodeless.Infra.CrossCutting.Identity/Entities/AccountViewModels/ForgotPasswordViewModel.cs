using System.ComponentModel.DataAnnotations;

namespace qodeless.Infra.CrossCutting.Identity.Entities.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
