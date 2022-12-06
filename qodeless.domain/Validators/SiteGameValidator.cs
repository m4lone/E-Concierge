using FluentValidation;
using qodeless.domain.Entities;
using System;

namespace qodeless.domain.Validators
{
    public class SiteGameAddValidator : AbstractValidator<SiteGame>
    {
        public SiteGameAddValidator()
        {
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.SiteId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteId).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.ESite).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.ESite).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class SiteGameUpdateValidator : AbstractValidator<SiteGame>
    {
        public SiteGameUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.ESite).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.ESite).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.SiteId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteId).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo não pode ser nulo");
        }

        public class SiteGameRemoveValidator : AbstractValidator<SiteGame>
        {
            public SiteGameRemoveValidator()
            {
                RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            }
        }
    }
}
