using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Account> GetAccounts()
        {
            return Db.Accounts.Where(_ => _.DeletedAt == DateTime.UnixEpoch && _.Excluded == false);
        }

        public IQueryable<UserPlaysRankingViewModel> UserPlaysRanking()
        {
            var result = new List<UserPlaysRankingViewModel>();
            var users = Db.Users.ToList();

            foreach (var user in users)
            {              
                var plays = Db.Plays.ToList().Where(_ => _.UserPlayId == user.Id).Count();
                result.Add(new UserPlaysRankingViewModel { User = user.NickName, Plays = plays });
            }

            return result.OrderByDescending(_ => _.Plays).AsQueryable();

        }
        public IQueryable<PartnersRankingViewModel> PartnersRanking()
        {
            var result = new List<PartnersRankingViewModel>();
            var partners = Db.Accounts.ToList();

            foreach (var partner in partners)
            {
                var royalties = partner.Royalties;
                result.Add(new PartnersRankingViewModel { Partner = partner.Name, Royalties = royalties });
            }

            return result.OrderByDescending(_ => _.Royalties).AsQueryable();

        }

        public IQueryable<ActivityStatusViewModel> GetOnlineUsers()
        {
            var result = new List<ActivityStatusViewModel>();
            var users = Db.Accounts.Where(_=>_.ActivityStatus == EActivityStatus.Online).ToList();

            foreach (var user in users)
            {
                result.Add(new ActivityStatusViewModel { User = user.Name, Status = user.ActivityStatus });
            }

            return result.OrderBy(_ => _.User).AsQueryable();

        }

    }
}
