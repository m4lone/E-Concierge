using AutoMapper;
using FluentValidation.Results;
using NLog;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class IncomeTypeAppService : IIncomeTypeAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IIncomeTypeRepository _incomeTypeRepository;
        public IncomeTypeAppService(IMapper mapper, IIncomeTypeRepository incomeTypeRepository)
        {
            _mapper = mapper;
            _incomeTypeRepository = incomeTypeRepository;
        }

        public IEnumerable<IncomeTypeViewModel> GetAll()
        {
            Logger.Info("== Type of income displayed on index ==");
            return _mapper.Map<IEnumerable<IncomeTypeViewModel>>(_incomeTypeRepository.GetAll());
        }

        public IncomeTypeViewModel GetById(Guid id)
        {
            return _mapper.Map<IncomeTypeViewModel>(_incomeTypeRepository.GetById(id));
        }

        public IEnumerable<IncomeTypeViewModel> GetAllBy(Func<IncomeType, bool> exp)
        {
            return _mapper.Map<IEnumerable<IncomeTypeViewModel>>(_incomeTypeRepository.GetAllBy(exp));
        }
        public ValidationResult Update(IncomeTypeViewModel vm)
        {
           Logger.Info("== Updating type of income ==");
            var entity = _mapper.Map<IncomeType>(vm);
            var validationResult = new IncomeTypeUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _incomeTypeRepository.Update(entity);

            return validationResult;
        }
        public ValidationResult Add(IncomeTypeViewModel vm)
        {
            Logger.Info("== Adding type of income ==");
            var entity = _mapper.Map<IncomeType>(vm);
            var validationResult = new IncomeTypeAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _incomeTypeRepository.Add(entity);

            return validationResult;
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(IncomeTypeViewModel vm)
        {
            Logger.Info("== Inserting type of income ==");
            if (Guid.Empty == vm.Id)
                return  Add(vm);
            else
                return  Update(vm);
        }

        public IEnumerable<IncomeTypeViewModel> GetIncomeTypes()
        {
            return _mapper.Map<IEnumerable<IncomeTypeViewModel>>(_incomeTypeRepository.GetIncomeTypes());
        }
    }
}
