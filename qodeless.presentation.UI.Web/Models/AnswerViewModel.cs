using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.presentation.WebApp.Models
{
    public class AnswerViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public bool IsRight { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public Guid QuestionId { get; set; }
    }
}
