using System.ComponentModel.DataAnnotations;

namespace qodeless.Infra.CrossCutting.Identity.Entities.AccountViewModels
{
    public class RegisterMobileViewModel
    {
        [Required]
        public string Phone { get; set; }
    }
}

