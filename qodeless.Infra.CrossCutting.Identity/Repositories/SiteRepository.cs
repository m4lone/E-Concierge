using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class SiteRepository : Repository<Site>, ISiteRepository
    {
        public SiteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Account> GetAccounts()
        {
            return Db.Accounts.Where(_ => _.DeletedAt == DateTime.UnixEpoch && _.Excluded == false);
        }
        public IQueryable<AccountDropDownViewModel> GetAccountsDropDown()
        {
            return Db.Accounts.Where(_ => _.DeletedAt == DateTime.UnixEpoch && _.Excluded == false)
                .Select(x => new AccountDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }

        public bool IsValidAccountId(Guid accountId)
        {
            return Db.Accounts.Any(c => c.Id == accountId);
        }

        public IEnumerable<SiteOperatorViewModel> GetOperators(Guid? employerId)
        {
            var siteName = Db.Sites
                .Where(x => x.Id == employerId && x.DeletedAt == DateTime.UnixEpoch && x.Excluded == false)
                .Select(x => x.Name).FirstOrDefault();

            return Db.Users.Where(x => x.EmployerId == employerId).Select(x => new SiteOperatorViewModel()
            {
                UserId = x.Id,
                Cpf = x.Cpf,
                Email = x.Email,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                SiteName = siteName
            });
        }

        public IQueryable<SiteRankingViewModel> SitesRanking()
        {
            var result = new List<SiteRankingViewModel>();
            var sites = Db.Sites.ToList();

            foreach (var site in sites)
            {
                var royalties = site.Royalties;
                result.Add(new SiteRankingViewModel { Site = site.Name, Royalties = royalties });
            }

            return result.OrderByDescending(_ => _.Royalties).AsQueryable();

        }
    }
}
