using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ISiteRepository : IRepository<Site> //SOLID
    {
        IQueryable<Account> GetAccounts();
        IQueryable<AccountDropDownViewModel> GetAccountsDropDown();
        bool IsValidAccountId(Guid accountId);
        IEnumerable<SiteOperatorViewModel> GetOperators(Guid? employerId);
        public IQueryable<SiteRankingViewModel> SitesRanking();
    }
}
