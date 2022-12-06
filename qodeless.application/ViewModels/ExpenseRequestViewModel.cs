using qodeless.domain.Entities;
using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.ViewModels
{
    public class ExpenseRequestViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ExpenseId { get; set; }
        public string UserOperationID { get; set; }
        public string Message { get; set; }
        public DateTime DueDate { get; set; }
        public EExpenseRequest Request { get; set; }
        public string ExpenseName { get; internal set; }
    }
}
