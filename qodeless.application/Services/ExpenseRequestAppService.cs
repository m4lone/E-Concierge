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
using Microsoft.EntityFrameworkCore;
using qodeless.domain.Model;
using NLog;

namespace qodeless.application
{
    public class ExpenseRequestAppService : IExpenseRequestAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IExpenseRequestRepository _expenseRequestRepository;
        public ExpenseRequestAppService(IMapper mapper, IExpenseRequestRepository ExpenseRepository)
        {
            _mapper = mapper;
            _expenseRequestRepository = ExpenseRepository;
        }

        public IEnumerable<ExpenseRequestViewModel> GetAll()
        {
           // Logger.Info("== Data (ExpenseRequest) that's displayed on index ==");
            var expense = _expenseRequestRepository.GetAll()
            .Include(_ => _.Expense)
            .Select(_ => new ExpenseRequestViewModel
            {
                Id = _.Id,
                UserOperationID = _.UserOperationID,
                DueDate = _.DueDate,
                Message = _.Message,
                Request = _.Request,
                ExpenseId = _.ExpenseId,
                ExpenseName = _.Expense.Site.Name,
            });
            return expense;
        }

        public ExpenseRequestViewModel GetById(Guid id)
        {
            return _mapper.Map<ExpenseRequestViewModel>(_expenseRequestRepository.GetById(id));
        }

        public IEnumerable<ExpenseRequestViewModel> GetAllBy(Func<ExpenseRequest, bool> exp)
        {
            return _mapper.Map<IEnumerable<ExpenseRequestViewModel>>(_expenseRequestRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
           // Logger.Info("== Removing data (ExpenseRequest) ==");
            var entity = _expenseRequestRepository.GetById(id);
            var validationResult = new ExpenseRequestRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _expenseRequestRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(ExpenseRequestViewModel vm)
        {
           // Logger.Info("== Adding data (ExpenseRequest) ==");
            var entity = _mapper.Map<ExpenseRequest>(vm);
            var validationResult = new ExpenseRequestAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _expenseRequestRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(ExpenseRequestViewModel vm)
        {
           // Logger.Info("== Updating data (ExpenseRequest) ==");
            var entity = _mapper.Map<ExpenseRequest>(vm);
            var validationResult = new ExpenseRequestUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _expenseRequestRepository.Update(entity);

            return validationResult;
        }
        public IEnumerable<ExpenseViewModel> GetExpenseRequests()
        {
            return _mapper.Map<IEnumerable<ExpenseViewModel>>(_expenseRequestRepository.GetExpenses());
        }
        public IEnumerable<SiteViewModel> GetSites()
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_expenseRequestRepository.GetSites());
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(ExpenseRequestViewModel vm)
        {
            Logger.Info("== Inserting data (ExpenseRequest) ==");
            if (Guid.Empty == vm.Id)
                return  Add(vm);
            else
                return  Update(vm);
        }
        public IEnumerable<ExpenseDropDownViewModel> GetExpensesDrop()
        {
            return _expenseRequestRepository.GetExpensesDrop();
        }

    }
}
