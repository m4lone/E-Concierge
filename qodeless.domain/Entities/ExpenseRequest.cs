using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.domain.Entities
{
    public class ExpenseRequest : Entity, IEntityTypeConfiguration<ExpenseRequest>
    {
        public virtual Expense Expense { get; set; }
        public Guid ExpenseId { get; set; }
        public string UserOperationID { get; set; }
        public string Message { get; set; }
        public DateTime DueDate { get; set; }
        public EExpenseRequest Request { get; set; }
        public ExpenseRequest(Guid id) { Id = id; }

        public ExpenseRequest(Guid expenseId, string message, DateTime dueDate, EExpenseRequest request)
        {
            ExpenseId = expenseId;
            Message = message;
            DueDate = dueDate;
            Request = request;
        }

        public void Configure(EntityTypeBuilder<ExpenseRequest> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DueDate).IsRequired();
            builder.Property(x => x.Request).IsRequired();

        }
    }
}