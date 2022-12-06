using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;

namespace qodeless.domain.Validators
{
    public class VirtualBalanceAddValidator : AbstractValidator<VirtualBalance>
    {
        public VirtualBalanceAddValidator()
        {
            RuleFor(entity => entity.UserPlayId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class VirtualBalanceUpdateValidator : AbstractValidator<VirtualBalance>
    {
        public VirtualBalanceUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class VirtualBalanceRemoveValidator : AbstractValidator<VirtualBalance>
    {
        public VirtualBalanceRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
