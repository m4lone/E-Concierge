using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System;

namespace qodeless.domain.Validators
{
    public class IncomeAddValidator : IncomeValidatorBase
    {
        public IncomeAddValidator(IIncomeRepository incomeRepository)
            : base(incomeRepository)
        {
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
            //RuleFor(entity => entity.SiteId).Must(IsValidSite).WithMessage("Site não localizado");
        }       
    }

    public class IncomeUpdateValidator : IncomeValidatorBase
    {
        public IncomeUpdateValidator(IIncomeRepository incomeRepository)
              : base(incomeRepository)
        {
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
            //RuleFor(entity => entity.SiteId).Must(IsValidSite).WithMessage("Site não localizado");
        }
    }

    public class IncomeRemoveValidator : IncomeValidatorBase
    {
        public IncomeRemoveValidator(IIncomeRepository incomeRepository)
            : base(incomeRepository)

        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }

    public abstract class IncomeValidatorBase : AbstractValidator<Income>
    {
        protected readonly IIncomeRepository _incomeRepository;
        public IncomeValidatorBase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        protected bool IsValidAccount(Guid? accountId) => _incomeRepository.IsValidAccountId(accountId);
        protected bool IsValidSite(Guid? siteId) => _incomeRepository.IsValidSiteId(siteId);
        protected bool IsValidIncomeType(Guid incometypeId) => _incomeRepository.IsValidIncomeTypeId(incometypeId);
    }
}
