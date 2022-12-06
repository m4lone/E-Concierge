using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application.Interfaces
{
    public interface IIncomeTypeAppService : IDisposable
    {
        IEnumerable<IncomeTypeViewModel> GetAll();
        IncomeTypeViewModel GetById(Guid id);
        IEnumerable<IncomeTypeViewModel> GetAllBy(Func<IncomeType, bool> exp);
        ValidationResult Add(IncomeTypeViewModel vm);
        ValidationResult Update(IncomeTypeViewModel vm);
        ValidationResult Upsert(IncomeTypeViewModel vm);
        IEnumerable<IncomeTypeViewModel> GetIncomeTypes();
    }
}
