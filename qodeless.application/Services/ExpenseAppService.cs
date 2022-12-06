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
using System.Threading.Tasks;
using qodeless.domain.Enums;
using Microsoft.EntityFrameworkCore;
using qodeless.Infra.CrossCutting.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace qodeless.application
{
    public class ExpenseAppService : IExpenseAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseRequestRepository _expenseRequestRepository;
        private readonly ISiteRepository _siteRepository;
        public ExpenseAppService(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IExpenseRepository expenseRepository, IExpenseRequestRepository expenseRequest, ISiteRepository siteRepository)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _expenseRepository = expenseRepository;
            _siteRepository = siteRepository;
            _expenseRequestRepository = expenseRequest;
        }

        public IEnumerable<ExpenseViewModel> GetAll()
        {
            Logger.Info("== Expenses displayed on the index ==");
            var expense = _expenseRepository.GetAll()
                .Select(_ => new ExpenseViewModel
                {
                    Id = _.Id,
                    Amount = _.Amount,
                    SiteId = _.SiteId,
                });
            return expense;
        }

        public ExpenseViewModel GetById(Guid id)
        {
            return _mapper.Map<ExpenseViewModel>(_expenseRepository.GetById(id));
        }

        public IEnumerable<ExpenseViewModel> GetAllBy(Func<Expense, bool> exp)
        {
            return _mapper.Map<IEnumerable<ExpenseViewModel>>(_expenseRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
           Logger.Info("== Removing expense ==");
            var entity = _expenseRepository.GetById(id);
            var validationResult = new ExpenseRemoveValidator(_expenseRepository).Validate(entity);
            if (validationResult.IsValid)
                _expenseRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(ExpenseViewModel vm)
        {
            Logger.Info("== Adding expense ==");
            var entity = _mapper.Map<Expense>(vm);
            var validationResult = new ExpenseAddValidator(_expenseRepository).Validate(entity);
            if (validationResult.IsValid)
                _expenseRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(ExpenseViewModel vm)
        {
            Logger.Info("== Updating expense ==");
            var entity = _mapper.Map<Expense>(vm);
            var validationResult = new ExpenseUpdateValidator(_expenseRepository).Validate(entity);
            if (validationResult.IsValid)
                _expenseRepository.Update(entity);

            return validationResult;
        }
        public IEnumerable<SiteViewModel> GetSites()
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_expenseRepository.GetSites());
        }

        public ValidationResult Upsert(ExpenseViewModel vm)
        {
            Logger.Info("== Inserting expense ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult AddExpenseSiteAdmin(ExpenseViewModel vm, Guid userId)
        {
            Logger.Info("== Adding expense into Site Admin ==");
            var entity = _mapper.Map<Expense>(vm);
            var user = _userManager.FindByIdAsync(userId.ToString());
            var perfil = _userManager.GetRolesAsync(user.Result);

            if (perfil.Result.Contains("SITE ADMIN"))
            {
                Logger.Info("== New data has been added into Site Admin==");
                //entity.Status = EExpenseStatus.Approved;
            }

            var validationResult = new ExpenseAddValidator(_expenseRepository).Validate(entity);
            if (validationResult.IsValid)
                _expenseRepository.Add(entity);

            return validationResult;
        }

        public ValidationResult AddExpensePartner(ExpenseViewModel vm, Guid userId)
        {
            Logger.Info("== Adding expense into Partner ==");
            var entity = _mapper.Map<Expense>(vm);
            var user = _userManager.FindByIdAsync(userId.ToString());
            var perfil = _userManager.GetRolesAsync(user.Result);

            if (perfil.Result.Contains("PARTNER"))
            {
                Logger.Info("== New data has been added into Partner ==");
                //entity.Status = EExpenseStatus.Approved;
            }

            var validationResult = new ExpenseAddValidator(_expenseRepository).Validate(entity);
            if (validationResult.IsValid)
                _expenseRepository.Add(entity);

            return validationResult;
        }
        public IEnumerable<ExpenseViewModel> GetExpense(Guid siteId)
        {
            return _mapper.Map<IEnumerable<ExpenseViewModel>>(_expenseRepository.GetAllBy(c => c.SiteId == siteId));
        }
    }
}

