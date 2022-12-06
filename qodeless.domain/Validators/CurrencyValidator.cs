using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class CurrencyAddValidator : AbstractValidator<Currency>
    {
        public CurrencyAddValidator()
        {
            RuleFor(entity => entity.VlrToBRL).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
        }
    }

    public class CurrencyUpdateValidator : AbstractValidator<Currency>
    {
        public CurrencyUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.VlrToBRL).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class CurrencyRemoveValidator : AbstractValidator<Currency>
    {
        public CurrencyRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
