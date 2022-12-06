using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;

namespace qodeless.application.Interfaces
{
    public interface IExpenseAppService : IDisposable
    {
        IEnumerable<ExpenseViewModel> GetAll();
        ExpenseViewModel GetById(Guid id);
        IEnumerable<ExpenseViewModel> GetAllBy(Func<Expense, bool> exp);
        ValidationResult Add(ExpenseViewModel vm);
        ValidationResult Update(ExpenseViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<SiteViewModel> GetSites();
        IEnumerable<ExpenseViewModel> GetExpense(Guid siteId);
        ValidationResult Upsert(ExpenseViewModel vm);
        ValidationResult AddExpenseSiteAdmin(ExpenseViewModel vm, Guid userId);
        ValidationResult AddExpensePartner(ExpenseViewModel vm, Guid userId);
    }
}
