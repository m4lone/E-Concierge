using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class AccountGameAddValidator : AbstractValidator<AccountGame>
    {
        public AccountGameAddValidator()
        {
            RuleFor(entity => entity.AccountId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
        }
    }

    public class AccountGameUpdateValidator : AbstractValidator<AccountGame>
    {
        public AccountGameUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.AccountId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
        }
    }

    public class AccountGameRemoveValidator : AbstractValidator<AccountGame>
    {
        public AccountGameRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
