using qodeless.domain.Entities;
using qodeless.domain.Model;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account> //SOLID
    {
        public bool UpdateBlockAccount(Account obj, bool bCommit = true)
        {
            obj.Status = domain.Enums.EAccountStatus.Blocked;
            return Update(obj, bCommit);
        }   
        public bool UpdateUnlockAccount(Account obj, bool bCommit = true)
        {
            obj.Status = domain.Enums.EAccountStatus.Actived;
            return Update(obj, bCommit);
        }
        IQueryable<Account> GetAccounts();

        public IQueryable<PartnersRankingViewModel> PartnersRanking();
        public IQueryable<UserPlaysRankingViewModel> UserPlaysRanking();
        public IQueryable<ActivityStatusViewModel> GetOnlineUsers();

    }
}
