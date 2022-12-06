using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.presentation.WebApp.Models
{
    public class GroupViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int AcceptanceCriteria { get; set; }
    }
}
