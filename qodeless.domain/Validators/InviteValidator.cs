using FluentValidation;
using qodeless.domain.Entities;
using System.Text.RegularExpressions;

namespace qodeless.domain.Validators
{
    public class InviteAddValidator : AbstractValidator<Invite>
    {
        public InviteAddValidator()
        {
            RuleFor(entity => entity.InviteToken).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.InviteToken).NotNull().WithMessage("Campo obrigatório");


        }
    }

    public class InviteUpdateValidator : AbstractValidator<Invite>
    {
        public InviteUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Id).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.InviteToken).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.InviteToken).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class InviteRemoveValidator : AbstractValidator<Invite>
    {
        public InviteRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Id).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.InviteToken).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.InviteToken).NotNull().WithMessage("Campo obrigatório");
        }                            
    }
}
