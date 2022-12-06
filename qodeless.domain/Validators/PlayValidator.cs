using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class PlayAddValidator : AbstractValidator<Play>
    {
        public PlayAddValidator()
        {
            RuleFor(entity => entity.UserPlayId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class PlayUpdateValidator : AbstractValidator<Play>
    {
        public PlayUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class PlayRemoveValidator : AbstractValidator<Play>
    {
        public PlayRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
