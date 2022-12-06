using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class IncomeRepository : Repository<Income>, IIncomeRepository
    {    
        public IncomeRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Income> GetIncomes()
        {
            return Db.Incomes;
        }
        public IQueryable<SiteDropDownViewModel> GetSitesDrop()
        {
            return Db.Sites.Select(x => new SiteDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }
        public IQueryable<AccountDropDownViewModel> GetAccountsDrop()
        {
            return Db.Accounts.Select(x => new AccountDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }
        public IQueryable<IncomeTypeDropDownViewModel> GetIncomeTypesDrop()
        {
            return Db.IncomeTypes.Select(x => new IncomeTypeDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }
        public bool IsValidIncomeId(Guid incomeId)
        {
            return Db.Incomes.Any(j => j.Id == incomeId);
        }
        public bool IsValidAccountId(Guid? accountId)
        {
            return Db.Accounts.Any(c => c.Id == accountId);
        }

        public bool IsValidSiteId(Guid? siteId)
        {
            return Db.Sites.Any(c => c.Id == siteId);
        }

        public bool IsValidIncomeTypeId(Guid incometypeId)
        {
            return Db.IncomeTypes.Any(c => c.Id == incometypeId);
        }
    }
}
