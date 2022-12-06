using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IExpenseRequestRepository : IRepository<ExpenseRequest>
    {
        IQueryable<Expense> GetExpenses();
        IQueryable<Site> GetSites();
        bool IsValidRequestId(Guid expenseId);
        IQueryable<ExpenseDropDownViewModel> GetExpensesDrop();
    }
}
