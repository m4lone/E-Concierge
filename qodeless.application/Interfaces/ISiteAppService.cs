using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface ISiteAppService : IDisposable
    {
        IEnumerable<SiteViewModel> GetAll();
        IEnumerable<SiteViewModel> GetAllIndex();
        SiteViewModel GetById(Guid id);
        IEnumerable<SiteViewModel> GetAllBy(Func<Site, bool> exp);
        ValidationResult Add(SiteViewModel vm);
        ValidationResult Update(SiteViewModel vm);
        ValidationResult Upsert(SiteViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<AccountViewModel> GetAccounts();
        IEnumerable<AccountDropDownViewModel> GetAccountsDropDown();
        public SiteViewModel GetTotalSites();
        SiteViewModel GetBy(Func<Site, bool> exp);
        ValidationResult UpdateCurrencyGameBySiteId(CurrencyGameViewModel currencyGame);
        List<int> GetCurrencyGame(Guid siteId);
        IEnumerable<SiteOperatorViewModel> GetOperators(Guid? employerId);
        public IEnumerable<SiteRankingViewModel> GetSitesRanking();
        public string CreatePassword(int length);
    }
}