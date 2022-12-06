using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qodeless.Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using qodeless.domain.Enums;
using NLog;

namespace qodeless.application
{
    public class IncomeAppService : IIncomeAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIncomeRepository _incomeRepository;
        public IncomeAppService(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IIncomeRepository incomeRepository)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _incomeRepository = incomeRepository;
        }

        public IEnumerable<IncomeViewModel> GetAll()
        {
            Logger.Info("== Income displayed on the index ==");
            var income = _incomeRepository.GetAll()
                .Include(_ => _.Account)
                .Include(_ => _.Site)
                .Select(_ => new IncomeViewModel
                {
                    Id = _.Id,
                    Type = _.Type,
                    Amount = _.Amount,
                    Description = _.Description,
                    SiteId = _.SiteId,
                    SiteName = _.Site.Name,
                    AccountId = _.AccountId,
                    AccountName = _.Account.Name,
                }).AsNoTracking();
            Dispose();
            return income;
        }

        public IncomeViewModel GetById(Guid id)
        {
            return _mapper.Map<IncomeViewModel>(_incomeRepository.GetById(id));
        }

        public IEnumerable<IncomeViewModel> GetAllBy(Func<Income, bool> exp)
        {
            return _mapper.Map<IEnumerable<IncomeViewModel>>(_incomeRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
           Logger.Info("== Removing income ==");
            var entity = _incomeRepository.GetById(id);
            var validationResult = new IncomeRemoveValidator(_incomeRepository).Validate(entity);
            if (validationResult.IsValid)
                _incomeRepository.SoftDelete(entity);

            return validationResult;
        }

        public ValidationResult Add(IncomeViewModel vm)
        {
           Logger.Info("== Adding income ==");
            var entity = _mapper.Map<Income>(vm);
            var validationResult = new IncomeAddValidator(_incomeRepository).Validate(entity);
            if (validationResult.IsValid)
                _incomeRepository.Add(entity);

            return validationResult;
        }

        public ValidationResult Update(IncomeViewModel vm)
        {
            Logger.Info("== Updating income ==");
            var entity = _mapper.Map<Income>(vm);
            var validationResult = new IncomeUpdateValidator(_incomeRepository).Validate(entity);
            if (validationResult.IsValid)
                _incomeRepository.Update(entity);

            return validationResult;
        }

        public void Dispose() => GC.SuppressFinalize(this);
        public ValidationResult Upsert(IncomeViewModel vm)
        {
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }

        public ValidationResult AddIncomePartner(IncomeViewModel vm)
        {
            Logger.Info("== Adding income into Partner ==");
            var entity = _mapper.Map<Income>(vm);
            //var user = _userManager.FindByIdAsync(vm.UserOperationId);
            //var perfil = _userManager.GetRolesAsync(user.Result);

            //if (perfil.Result.Contains("PARTNER"))
            //{
            //    entity.Status = EIncomeStatus.Approved;
            //    Logger.Info("== New data's added into Partner ==");
            //}

            var validationResult = new IncomeAddValidator(_incomeRepository).Validate(entity);
            if (validationResult.IsValid)
                _incomeRepository.Add(entity);

            return validationResult;
        }

        public ValidationResult CheckedIncome(IncomeViewModel vm)
        {
            var entity = _mapper.Map<Income>(vm);
            var validationResult = new IncomeAddValidator(_incomeRepository).Validate(entity);

            if (validationResult.IsValid)
            {
                _incomeRepository.Add(entity);
            }
            return validationResult;
        }

        public IEnumerable<SiteDropDownViewModel> GetSitesDrop()
        {
            return _incomeRepository.GetSitesDrop();
        }

        public IEnumerable<IncomeTypeDropDownViewModel> GetIncomeTypesDrop()
        {
            return _incomeRepository.GetIncomeTypesDrop();
        }

        public IEnumerable<AccountDropDownViewModel> GetAccountsDrop()
        {
            return _incomeRepository.GetAccountsDrop();
        }

        public IEnumerable<IncomeViewModel> GetIncome(Guid siteId)
        {
            return _mapper.Map<IEnumerable<IncomeViewModel>>(_incomeRepository.GetAllBy(c => c.SiteId == siteId));
        }

    }
}
