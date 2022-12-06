using System.ComponentModel.DataAnnotations;

namespace qodeless.Infra.CrossCutting.Identity.Entities.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
