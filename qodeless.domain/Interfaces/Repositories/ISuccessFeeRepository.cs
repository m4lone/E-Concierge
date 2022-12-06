using qodeless.domain.Entities;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ISuccessFeeRepository : IRepository<SuccessFee> 
    {
        IQueryable<Account> GetAccounts();
        bool IsValidAccountId(Guid accountId);
        IQueryable<Site> GetSites();
        bool IsValidSiteId(Guid siteId);
        IQueryable<SuccessFee> GetUsers();
        bool IsValidSiteId(string userId);
        
        double IsValidTotalFeeByAccountId(Guid accountId);

    }
}
