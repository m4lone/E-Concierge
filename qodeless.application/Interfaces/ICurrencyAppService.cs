using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application.Interfaces
{
    public interface ICurrencyAppService : IDisposable
    {
        IEnumerable<CurrencyViewModel> GetAll();
        CurrencyViewModel GetById(Guid id);
        IEnumerable<CurrencyViewModel> GetAllBy(Func<Currency, bool> exp);
        ValidationResult Add(CurrencyViewModel vm);
        ValidationResult Update(CurrencyViewModel vm);
        ValidationResult Remove(Guid id);
        ValidationResult Upsert(CurrencyViewModel vm);
    }
}
