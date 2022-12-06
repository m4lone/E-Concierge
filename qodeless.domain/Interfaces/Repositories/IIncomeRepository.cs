using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IIncomeRepository : IRepository<Income> 
    {   
        IQueryable<Income> GetIncomes();
        IQueryable<SiteDropDownViewModel> GetSitesDrop();
        IQueryable<AccountDropDownViewModel> GetAccountsDrop();
        IQueryable<IncomeTypeDropDownViewModel> GetIncomeTypesDrop();
        bool IsValidIncomeId(Guid incomeId);
        bool IsValidAccountId(Guid? accountId);
        bool IsValidSiteId(Guid? siteId);
        bool IsValidIncomeTypeId(Guid incometypeId);

        
    }
}
