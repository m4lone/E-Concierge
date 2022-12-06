using System.ComponentModel.DataAnnotations;

namespace qodeless.Infra.CrossCutting.Identity.Entities.AccountViewModels
{
    public class LoginUnityViewModel
    {
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
