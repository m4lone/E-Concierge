using AutoMapper;
using FluentValidation.Results;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class VirtualBalanceAppService : IVirtualBalanceAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IVirtualBalanceRepository _virtualBalanceRepository;
        private readonly ISitePlayerAppService _sitePlayerAppService;
        public VirtualBalanceAppService(IMapper mapper, IVirtualBalanceRepository virtualBalanceRepository, ISitePlayerAppService sitePlayerAppService)
        {
            _mapper = mapper;
            _virtualBalanceRepository = virtualBalanceRepository;
            _sitePlayerAppService = sitePlayerAppService;
        }

        public IEnumerable<VirtualBalanceViewModel> GetAll()
        {
            Logger.Info("== Virtual balance displayed on the index ==");
            return _mapper.Map<IEnumerable<VirtualBalanceViewModel>>(_virtualBalanceRepository.GetAll());
        }

        public VirtualBalanceViewModel GetById(Guid id)
        {
            return _mapper.Map<VirtualBalanceViewModel>(_virtualBalanceRepository.GetById(id));
        }
       
        public bool PoolingForPendingBalancesByUserPlayerId(Guid userPlayerId)
        {//TODO: Implementar ViewModel com sucess da operação e bool do resultado da qeue
            return _virtualBalanceRepository.GetAllBy(x => x.UserPlayId == userPlayerId.ToString()).Where(x => x.BalanceState == EBalanceState.Pending).Any();
        }

        public double PoolingSumAmountByUserPlayerId(Guid userPlayerId)
        { //TODO: Implementar ViewModel com sucess da operação e bool do resultado da qeue
            var debit = _virtualBalanceRepository.GetAllBy(x => x.UserPlayId == userPlayerId.ToString())
            .Where(x => x.Type == EBalanceType.Debit)
            .Where(x => x.BalanceState != EBalanceState.Canceled)
            .Sum(c => c.Amount);

            var credit = _virtualBalanceRepository.GetAllBy(x => x.UserPlayId == userPlayerId.ToString())
            .Where(x => x.Type == EBalanceType.Credit)
            .Where(x => x.BalanceState != EBalanceState.Canceled)
            .Sum(c => c.Amount);

            return credit + (debit * -1);
        }

        public IEnumerable<VirtualBalanceViewModel> GetAllBy(Func<VirtualBalance, bool> exp)
        {
            return _mapper.Map<IEnumerable<VirtualBalanceViewModel>>(_virtualBalanceRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing virtual balance ==");
            var entity = _virtualBalanceRepository.GetById(id);
            var validationResult = new VirtualBalanceRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _virtualBalanceRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(VirtualBalanceViewModel vm)
        {
            Logger.Info("== Adding virtual balance ==");
            var entity = _mapper.Map<VirtualBalance>(vm);
            var validationResult = new VirtualBalanceAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _virtualBalanceRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(VirtualBalanceViewModel vm)
        {
            Logger.Info("== Updating virtual balance ==");
            var entity = _mapper.Map<VirtualBalance>(vm);
            var validationResult = new VirtualBalanceUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _virtualBalanceRepository.Update(entity);

            return validationResult;
        }
        public ValidationResult Upsert(VirtualBalanceViewModel vm)
        {
            Logger.Info("== Inserting virtual balance ==");
            if (Guid.Empty == vm.Id)
            {
                return  Add(vm);
            }
            else
            {
                return  Update(vm);
            }
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public VirtualBalanceViewModel GetTotalBalances()
        {
           Logger.Info("== Getting the total of the balance ==");
            var totalBalances = _virtualBalanceRepository.GetAll().Select(c => c.Amount).Sum();
            return new VirtualBalanceViewModel { TotalBalances = totalBalances };

        }

        public int? GetAndUpdateTotalPendingBalances(string userId,Guid siteId)
        {
            try
            {
                Logger.Info("== Getting the total of the pending balances ==");
                var pendingBalances = _virtualBalanceRepository.GetAllBy(x => x.UserPlayId.Equals(userId) && x.BalanceState.Equals(EBalanceState.Pending)).ToList();
                var sitePlayer = _sitePlayerAppService.GetBySiteIdPlayerId(siteId, userId);
                foreach (var pendingBalance in pendingBalances)
                {
                    pendingBalance.BalanceState = EBalanceState.Paid;
                    if (_virtualBalanceRepository.Update(pendingBalance))
                    {
                        sitePlayer.CreditAmount += (int)(pendingBalance.Amount * 100);
                    }
                }
                var result = _sitePlayerAppService.Update(sitePlayer);

                return result.IsValid ? sitePlayer.CreditAmount : null;
            }
            catch (Exception e)
            {
                Logger.Error("GetAndUpdateTotalPendingBalances -> " + e.Message);
                return null;
            }

        }
    }
}
