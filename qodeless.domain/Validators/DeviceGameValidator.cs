using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;

namespace qodeless.domain.Validators
{
    public class DeviceGameAddValidator : AbstractValidator<DeviceGame>
    {
        public DeviceGameAddValidator()
        {
            RuleFor(entity => entity.DeviceId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DeviceId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class DeviceGameUpdateValidator : AbstractValidator<DeviceGame>
    {
        public DeviceGameUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DeviceId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DeviceId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class DeviceGameRemoveValidator : AbstractValidator<DeviceGame>
    {
        public DeviceGameRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
