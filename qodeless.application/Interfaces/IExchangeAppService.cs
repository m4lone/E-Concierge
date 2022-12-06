using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;

namespace qodeless.application.Interfaces
{
    public interface IExchangeAppService : IDisposable
    {
        IEnumerable<ExchangeViewModel> GetAll();
        ExchangeViewModel GetById(Guid id);
        IEnumerable<ExchangeViewModel> GetAllBy(Func<Exchange, bool> exp);
        ValidationResult Add(ExchangeViewModel vm);
        ValidationResult Update(ExchangeViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<ExchangeViewModel> GetExchangeByPartner(Guid partnerId);
    }
}
