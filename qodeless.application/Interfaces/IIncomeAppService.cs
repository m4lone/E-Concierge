using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IIncomeAppService : IDisposable
    {
        IEnumerable<IncomeViewModel> GetAll();
        IncomeViewModel GetById(Guid id);
        IEnumerable<SiteDropDownViewModel> GetSitesDrop();
        IEnumerable<AccountDropDownViewModel> GetAccountsDrop();
        IEnumerable<IncomeTypeDropDownViewModel> GetIncomeTypesDrop();
        IEnumerable<IncomeViewModel> GetAllBy(Func<Income, bool> exp);
        ValidationResult Add(IncomeViewModel vm);
        ValidationResult Update(IncomeViewModel vm);
        ValidationResult Upsert(IncomeViewModel vm);
        ValidationResult Remove(Guid id);
        ValidationResult AddIncomePartner(IncomeViewModel vm);
        ValidationResult CheckedIncome(IncomeViewModel vm);
        IEnumerable<IncomeViewModel> GetIncome(Guid siteId);
    }
}
