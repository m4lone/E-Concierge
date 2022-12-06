using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebApi.Model
{
    public class QuestionsViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Step { get; set; }
        public int Weight { get; set; }
        public Guid QuizId { get; set; }
        public EStatus Status { get; set; }
    }
}
