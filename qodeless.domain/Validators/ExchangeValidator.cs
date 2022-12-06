using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System;

namespace qodeless.domain.Validators
{
    public class ExchangeAddValidator : ExchangeValidatorBase
    {
        public ExchangeAddValidator(IExchangeRepository exchangeRepository) : base(exchangeRepository)
        {
            RuleFor(entity => entity.Note).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Note).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.AccountId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.AccountId).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
            //RuleFor(entity => entity.SiteId).Must(IsValidSite).WithMessage("Site não localizado");
        }
    }

    public class ExchangeUpdateValidator : ExchangeValidatorBase
    {
        public ExchangeUpdateValidator(IExchangeRepository exchangeRepository) : base(exchangeRepository)
        {
            RuleFor(entity => entity.Note).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Note).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.AccountId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.AccountId).NotNull().WithMessage("Campo obrigatório");
            //RuleFor(entity => entity.AccountId).Must(IsValidAccount).WithMessage("Conta não localizada");
            //RuleFor(entity => entity.SiteId).Must(IsValidSite).WithMessage("Site não localizado");
        }
    }

    public class ExchangeRemoveValidator : ExchangeValidatorBase
    {
        public ExchangeRemoveValidator(IExchangeRepository exchangeRepository) : base(exchangeRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }

    public abstract class ExchangeValidatorBase : AbstractValidator<Exchange>
    {
        protected readonly IExchangeRepository _exchangeRepository;
        public ExchangeValidatorBase(IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }

        protected bool IsValidAccount(Guid? accountId) => _exchangeRepository.IsValidAccountId(accountId);
    }
}
