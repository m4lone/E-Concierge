using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class GameAddValidator : AbstractValidator<Game>
    {
        public GameAddValidator()
        {
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtPublish).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class GameUpdateValidator : AbstractValidator<Game>
    {
        public GameUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Name).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtPublish).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class GameRemoveValidator : AbstractValidator<Game>
    {
        public GameRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
