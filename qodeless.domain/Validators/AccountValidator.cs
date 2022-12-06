using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class AccountAddValidator : AbstractValidator<Account>
    {
        public AccountAddValidator()
        {
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Description).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class AccountUpdateValidator : AbstractValidator<Account>
    {
        public AccountUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Description).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class AccountRemoveValidator : AbstractValidator<Account>
    {
        public AccountRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
