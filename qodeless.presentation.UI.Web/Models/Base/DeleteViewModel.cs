using System;

namespace qodeless.presentation.UI.Web.Models
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {
            CanDelete = true;
            Message = string.Empty;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public bool CanDelete { get; set; }
        public string Message { get; set; }

    }
}
