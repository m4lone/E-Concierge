using AutoMapper;
using FluentValidation.Results;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class SuccessFeeAppService : ISuccessFeeAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ISuccessFeeRepository _successFeeRepository;
        public SuccessFeeAppService(IMapper mapper, ISuccessFeeRepository SuccessFeeRepository)
        {
            _mapper = mapper;
            _successFeeRepository = SuccessFeeRepository;
        }

        public IEnumerable<SuccessFeeViewModel> GetAll()
        {
            Logger.Info("== SucessFee displayed on index ==");
            return _mapper.Map<IEnumerable<SuccessFeeViewModel>>(_successFeeRepository.GetAll());
        }

        public SuccessFeeViewModel GetById(Guid id)
        {
           return  _mapper.Map<SuccessFeeViewModel>(_successFeeRepository.GetById(id));
            
        }
        public IEnumerable<SuccessFeeViewModel> GetByAccountId(Guid id)
        {
            var x = _mapper.Map<IEnumerable<SuccessFeeViewModel>>(_successFeeRepository.GetAllBy(c => c.AccountId == id));
            return x;
        }
        public IEnumerable<SuccessFeeViewModel> GetBySiteId(Guid id)
        {
            var x = _mapper.Map<IEnumerable<SuccessFeeViewModel>>(_successFeeRepository.GetAllBy(c => c.SiteId == id));
            return x;
        }
        public IEnumerable<SuccessFeeViewModel> GetByUserId(string userId)
        {
            var y = _mapper.Map<IEnumerable<SuccessFeeViewModel>>(_successFeeRepository.GetAllBy(c => c.UserId == userId));
            return y;
        }
        public Double GetTotalFeeByAccountId(Guid accountId)
        {
            var t = _mapper.Map<Double>(_successFeeRepository.GetAllBy(u => u.Id == accountId).Sum(c => c.Rate));
            return t;
        }

        public IEnumerable<SuccessFeeViewModel> GetAllBy(Func<SuccessFee, bool> exp)
        {
            return _mapper.Map<IEnumerable<SuccessFeeViewModel>>(_successFeeRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
           Logger.Info("== Removing Success fee ==");
            var entity = _successFeeRepository.GetById(id);
            var validationResult = new SuccessFeeRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _successFeeRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(SuccessFeeViewModel vm)
        {
            Logger.Info("== Adding Success fee ==");
            var entity = _mapper.Map<SuccessFee>(vm);
            var validationResult = new SuccessFeeAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _successFeeRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(SuccessFeeViewModel vm)
        {
            Logger.Info("== Updating Success fee ==");
            var entity = _mapper.Map<SuccessFee>(vm);
            var validationResult = new SuccessFeeUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _successFeeRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);
        public IEnumerable<AccountViewModel> GetAccounts()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(_successFeeRepository.GetAccounts());
        }
        public IEnumerable<SiteViewModel> GetSites()
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_successFeeRepository.GetSites());
        }

    }
}
