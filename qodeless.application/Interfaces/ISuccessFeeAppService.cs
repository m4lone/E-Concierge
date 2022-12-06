using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application
{
    public interface ISuccessFeeAppService : IDisposable
    {
        IEnumerable<SuccessFeeViewModel> GetAll();
        SuccessFeeViewModel GetById(Guid id);
        IEnumerable<SuccessFeeViewModel> GetAllBy(Func<SuccessFee, bool> exp);
        ValidationResult Add(SuccessFeeViewModel vm);
        ValidationResult Update(SuccessFeeViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<AccountViewModel> GetAccounts();
        IEnumerable<SuccessFeeViewModel> GetByAccountId(Guid id);
        IEnumerable<SiteViewModel> GetSites();
        IEnumerable<SuccessFeeViewModel> GetBySiteId(Guid id);
        IEnumerable<SuccessFeeViewModel> GetByUserId(string userId);
        Double GetTotalFeeByAccountId(Guid id);
    }
}