using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;

namespace qodeless.application
{
    public interface IVouscherAppService : IDisposable
    {
        IEnumerable<VouscherViewModel> GetAllBySiteId(Func<Voucher, bool> exp);
        IEnumerable<VouscherViewModel> GetAll();
        VouscherViewModel GetById(Guid id);
        IEnumerable<VouscherViewModel> GetAllBy(Func<Voucher, bool> exp);
        VouscherViewModel GetBy(Func<Voucher, bool> exp);
        ValidationResult Add(VouscherViewModel vm);
        ValidationResult Update(VouscherViewModel vm);
        ValidationResult Remove(Guid id);
        string GenerateVoucherCode(VouscherViewModel vm);
        object VerifyPayVoucherCode(List<string> voucherCode, string userPlayId, EBalanceType type, string description, string userOperationId);
        ValidationResult DisableVoucher(Guid id);
    }
}
