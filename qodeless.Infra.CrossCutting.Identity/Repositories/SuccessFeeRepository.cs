using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class SuccessFeeRepository : Repository<SuccessFee>, ISuccessFeeRepository
    {
        public SuccessFeeRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Account> GetAccounts()
        {
            return Db.Accounts;
        }
 
        public bool IsValidAccountId(Guid accountId)
        {
            return Db.Accounts.Any(c => c.Id == accountId);
        }
        public IQueryable<Site> GetSites()
        {
            return Db.Sites;
        }

        public bool IsValidSiteId(Guid siteId)
        {
            return Db.Sites.Any(j => j.Id == siteId);
        }
        public IQueryable<SuccessFee> GetUsers()
        {
            return Db.SuccessFees;
        }
        public bool IsValidSiteId(string userId)
        {
            return Db.SuccessFees.Any(u => u.UserId == userId);
        }
        public double IsValidTotalFeeByAccountId(Guid accountId)
        {
            return Db.SuccessFees.Where(u => u.Id == accountId).Sum(c => c.Rate);
        }
    }
}
