using AutoMapper;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.application.Interfaces;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using qodeless.Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using NLog;
using qodeless.domain.Enums;

namespace qodeless.application
{
    public class ExchangeAppService : IExchangeAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IExchangeRepository _exchangeRepository;
        public ExchangeAppService(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IExchangeRepository exchangeRepository, ISiteRepository siteRepository)
        {
            _mapper = mapper;
            _exchangeRepository = exchangeRepository;
        }

        public IEnumerable<ExchangeViewModel> GetAll()
        {
            Logger.Info("== Exchanges displayed on the index ==");
            var exchange = _exchangeRepository.GetAll()
                .Where(_=>_.Excluded == false)
                .Select(_ => new ExchangeViewModel
                {
                    Id = _.Id,
                    Value = _.Value,
                    Note = _.Note,
                    AccountId = _.AccountId,
                });
            return exchange;
        }

        public ExchangeViewModel GetById(Guid id)
        {
            return _mapper.Map<ExchangeViewModel>(_exchangeRepository.GetById(id));
        }

        public IEnumerable<ExchangeViewModel> GetAllBy(Func<Exchange, bool> exp)
        {
            return _mapper.Map<IEnumerable<ExchangeViewModel>>(_exchangeRepository.GetAllBy(exp).Where(_ => _.Excluded == false));
        }

        public ValidationResult Remove(Guid id)
        {
           Logger.Info("== Removing exchange ==");
            var entity = _exchangeRepository.GetById(id);
            var validationResult = new ExchangeRemoveValidator(_exchangeRepository).Validate(entity);
            if (validationResult.IsValid)
                _exchangeRepository.SoftDelete(entity);

            return validationResult;
        }
        
        public ValidationResult Add(ExchangeViewModel vm)
        {
            Logger.Info("== Adding exchange ==");
            var entity = _mapper.Map<Exchange>(vm);
            var validationResult = new ExchangeAddValidator(_exchangeRepository).Validate(entity);
            if (validationResult.IsValid)
                _exchangeRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(ExchangeViewModel vm)
        {
            Logger.Info("== Updating exchange ==");
            var entity = _mapper.Map<Exchange>(vm);
            var validationResult = new ExchangeUpdateValidator(_exchangeRepository).Validate(entity);
            if (validationResult.IsValid)
                _exchangeRepository.Update(entity);

            return validationResult;
        }

        public ValidationResult Upsert(ExchangeViewModel vm)
        {
            Logger.Info("== Inserting exchange ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }

        public void Dispose() => GC.SuppressFinalize(this);
        public IEnumerable<ExchangeViewModel> GetExchangeByPartner(Guid partnerId)
        {
            return _mapper.Map<IEnumerable<ExchangeViewModel>>(_exchangeRepository.GetAllBy(c => c.AccountId == partnerId));
        }
    }
}

