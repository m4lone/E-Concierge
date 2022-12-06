using FluentValidation;
using qodeless.domain.Entities;
using System;

namespace qodeless.domain.Validators
{
    public class IncomeTypeAddValidator : AbstractValidator<IncomeType>
    {
        public IncomeTypeAddValidator()
        {
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class IncomeTypeUpdateValidator : AbstractValidator<IncomeType>
    {
        public IncomeTypeUpdateValidator()
        {
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }
}
