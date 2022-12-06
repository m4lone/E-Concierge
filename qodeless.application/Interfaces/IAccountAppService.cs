using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IAccountAppService : IDisposable
    {
        IEnumerable<AccountViewModel> GetAll();
        IEnumerable<SiteViewModel> GetSites(Guid accountId);
        IEnumerable<AccountViewModel> GetAllIndex();
        AccountViewModel GetById(Guid id);
        IEnumerable<AccountViewModel> GetAllBy(Func<Account, bool> exp);
        ValidationResult UpdateBlockAccount(Guid id);
        ValidationResult UpdateUnlockAccount(Guid id);
        ValidationResult Add(AccountViewModel vm);
        ValidationResult Update(AccountViewModel vm);
        ValidationResult Upsert(AccountViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<AccountViewModel> GetAccounts();
        public IEnumerable<PartnersRankingViewModel> GetPartnersRanking();
        public IEnumerable<UserPlaysRankingViewModel> GetUserPlaysRanking();
        public IEnumerable<ActivityStatusViewModel> GetOnlineUsers();
    }
}