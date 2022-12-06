using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class TokenAddValidator : AbstractValidator<Token>
    {
        public TokenAddValidator()
        {
            RuleFor(entity => entity.DtExpiration).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Phone).NotEmpty().WithMessage("Campo obrigatório");

        }
    }

    public class TokenUpdateValidator : AbstractValidator<Token>
    {
        public TokenUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtExpiration).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Phone).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class TokenRemoveValidator : AbstractValidator<Token>
    {
        public TokenRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
