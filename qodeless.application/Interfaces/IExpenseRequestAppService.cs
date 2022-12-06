using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IExpenseRequestAppService : IDisposable
    {
        IEnumerable<ExpenseRequestViewModel> GetAll();
        ExpenseRequestViewModel GetById(Guid id);
        IEnumerable<ExpenseRequestViewModel> GetAllBy(Func<ExpenseRequest, bool> exp);
        ValidationResult Add(ExpenseRequestViewModel vm);
        ValidationResult Update(ExpenseRequestViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<ExpenseViewModel> GetExpenseRequests();
        IEnumerable<SiteViewModel> GetSites();
        ValidationResult Upsert(ExpenseRequestViewModel vm);
        IEnumerable<ExpenseDropDownViewModel> GetExpensesDrop();

    }
}

