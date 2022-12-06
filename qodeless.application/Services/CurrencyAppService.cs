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
    public class CurrencyAppService : ICurrencyAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyAppService(IMapper mapper, ICurrencyRepository CurrencyRepository)
        {
            _mapper = mapper;
            _currencyRepository = CurrencyRepository;
        }

        public  IEnumerable<CurrencyViewModel> GetAll()
        {
           Logger.Info("== Data (currency) that's displayed on the index ==");
            var devices = _currencyRepository.GetCurrencys();
            return _mapper.Map<IEnumerable<CurrencyViewModel>>(devices);
        }

        public  CurrencyViewModel GetById(Guid id)
        {
            var currency = _currencyRepository.GetById(id);
            if (currency.DeletedAt != DateTime.UnixEpoch || currency.Excluded == true)
            {
                throw new Exception("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }
            return _mapper.Map<CurrencyViewModel>(currency);
        }

        public  IEnumerable<CurrencyViewModel> GetAllBy(Func<Currency, bool> exp)
        {
            return _mapper.Map<IEnumerable<CurrencyViewModel>>(_currencyRepository.GetAllBy(exp));
        }

        public  ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing data ==");
            var entity = _currencyRepository.GetById(id);
            var exist = GetById(id);
            var validationResult = new CurrencyRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _currencyRepository.SoftDelete(entity);

            return validationResult;
        }
        public  ValidationResult Add(CurrencyViewModel vm)
        {
           Logger.Info("== Adding data ==");
            var entity = _mapper.Map<Currency>(vm);
            var validationResult = new CurrencyAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _currencyRepository.Add(entity);

            return validationResult;
        }
        public  ValidationResult Update(CurrencyViewModel vm)
        {
           Logger.Info("== Updating data ==");
            var entity = _mapper.Map<Currency>(vm);
            var exist = GetById(vm.Id);
            var validationResult = new CurrencyUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _currencyRepository.Update(entity);

            return validationResult;
        }

        public  ValidationResult Upsert(CurrencyViewModel vm)
        {
            Logger.Info("== Inserting data ==");
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
    }
}
