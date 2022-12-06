using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;

namespace qodeless.application
{
    public interface IVirtualBalanceAppService : IDisposable
    {
        IEnumerable<VirtualBalanceViewModel> GetAll();
        VirtualBalanceViewModel GetById(Guid id);
        IEnumerable<VirtualBalanceViewModel> GetAllBy(Func<VirtualBalance, bool> exp);
        ValidationResult Add(VirtualBalanceViewModel vm);
        ValidationResult Update(VirtualBalanceViewModel vm);
        ValidationResult Upsert(VirtualBalanceViewModel vm);
        ValidationResult Remove(Guid id);
        public VirtualBalanceViewModel GetTotalBalances();
        bool PoolingForPendingBalancesByUserPlayerId(Guid userPlayerId);
        double PoolingSumAmountByUserPlayerId(Guid userPlayerId);
        int? GetAndUpdateTotalPendingBalances(string userId, Guid siteId);
    }
}