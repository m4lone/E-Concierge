using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Site> GetSites()
        {
            return Db.Sites;
        }
        public IQueryable<Expense> GetExpenses()
        {
            return Db.Expenses;
        }
        public bool IsValidSiteId(Guid? siteId)
        {
            return Db.Sites.Any(j => j.Id == siteId);
        }

        public bool IsValidAccountId(Guid? accountId)
        {
            return Db.Accounts.Any(j => j.Id == accountId);
        }
    }
}
