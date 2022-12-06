using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class SitePlayerAddValidator : AbstractValidator<SitePlayer>
    {
        public SitePlayerAddValidator()
        {
            RuleFor(entity => entity.UserPlayerId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayerId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class SitePlayerUpdateValidator : AbstractValidator<SitePlayer>
    {
        public SitePlayerUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayerId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayerId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class SitePlayerRemoveValidator : AbstractValidator<SitePlayer>
    {
        public SitePlayerRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
