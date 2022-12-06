using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class GroupAddValidator : AbstractValidator<Group>
    {
        public GroupAddValidator()
        {
            const int MIN_VALUE_SC0RE = 50;
            const int MIN_CODE_DIGITS = 2;

            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).MinimumLength(MIN_CODE_DIGITS).WithMessage($"Mínimo {MIN_CODE_DIGITS} caracteres");
            RuleFor(entity => entity.AcceptanceCriteria).GreaterThanOrEqualTo(MIN_VALUE_SC0RE).WithMessage("Valor mínimo: 50");
        }
    }

    public class GroupUpdateValidator : AbstractValidator<Group>
    {
        public GroupUpdateValidator()
        {
            const int MIN_VALUE_SC0RE = 50;
            const int MIN_CODE_DIGITS = 2;

            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).MinimumLength(MIN_CODE_DIGITS).WithMessage($"Mínimo {MIN_CODE_DIGITS} caracteres");
            RuleFor(entity => entity.AcceptanceCriteria).GreaterThanOrEqualTo(MIN_VALUE_SC0RE).WithMessage("Valor mínimo: 50");
        }
    }

    public class GroupRemoveValidator : AbstractValidator<Group>
    {
        public GroupRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
