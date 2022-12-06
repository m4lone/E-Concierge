using qodeless.domain.Entities;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IExpenseRepository : IRepository<Expense> 
    {
        IQueryable<Site> GetSites();
        IQueryable<Expense> GetExpenses();
        bool IsValidSiteId(Guid? siteId);
        bool IsValidAccountId(Guid? accountId);
    }
}
