using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System;

namespace qodeless.domain.Validators
{
    public class ExpenseAddValidator : ExpenseValidatorBase
    {
        public ExpenseAddValidator(IExpenseRepository expenseRepository) : base(expenseRepository)
        {
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
            //RuleFor(entity => entity.SiteId).Must(IsValidSite).WithMessage("Site não localizado");
        }
    }

    public class ExpenseUpdateValidator : ExpenseValidatorBase
    {
        public ExpenseUpdateValidator(IExpenseRepository expenseRepository) : base(expenseRepository)
        {
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
            //RuleFor(entity => entity.SiteId).Must(IsValidSite).WithMessage("Site não localizado");
        }
    }

    public class ExpenseRemoveValidator : ExpenseValidatorBase
    {
        public ExpenseRemoveValidator(IExpenseRepository expenseRepository) : base(expenseRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }

    public abstract class ExpenseValidatorBase : AbstractValidator<Expense>
    {
        protected readonly IExpenseRepository _expenseRepository;
        public ExpenseValidatorBase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        protected bool IsValidAccount(Guid? accountId) => _expenseRepository.IsValidAccountId(accountId);
        protected bool IsValidSite(Guid? siteId) => _expenseRepository.IsValidSiteId(siteId);
    }
}
