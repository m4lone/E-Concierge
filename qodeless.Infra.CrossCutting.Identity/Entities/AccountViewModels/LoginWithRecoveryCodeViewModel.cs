using System.ComponentModel.DataAnnotations;

namespace qodeless.Infra.CrossCutting.Identity.Entities.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
    }
}
