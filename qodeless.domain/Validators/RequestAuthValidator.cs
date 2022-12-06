using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class RequestAuthAddValidator : AbstractValidator<RequestAuth>
    {
        public RequestAuthAddValidator()
        {
            RuleFor(entity => entity.Username).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Password).NotEmpty().WithMessage("Valor obrigatório");
        }
    }

    public class RequestAuthUpdateValidator : AbstractValidator<RequestAuth>
    {
        public RequestAuthUpdateValidator()
        {
            RuleFor(entity => entity.Username).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Password).NotEmpty().WithMessage("Valor obrigatório");
        }
    }

    public class RequestAuthRemoveValidator : AbstractValidator<RequestAuth>
    {
        public RequestAuthRemoveValidator()
        {
        }
    }
}
