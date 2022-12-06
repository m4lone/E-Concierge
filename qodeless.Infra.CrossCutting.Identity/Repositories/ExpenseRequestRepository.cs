using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class ExpenseRequestRepository : Repository<ExpenseRequest>, IExpenseRequestRepository
    {
        public ExpenseRequestRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Expense> GetExpenses()
        {
            return Db.Expenses;
        }
        public IQueryable<Site> GetSites()
        {
            return Db.Sites;
        }

        public bool IsValidRequestId(Guid expenseId)
        {
            return Db.Expenses.Any(j => j.Id == expenseId);
        }
        public IQueryable<ExpenseDropDownViewModel> GetExpensesDrop()
        {
            return Db.Expenses.Select(x => new ExpenseDropDownViewModel { Id = x.Id, Name = x.Id.ToString()}).AsQueryable();
        }
    }
}
