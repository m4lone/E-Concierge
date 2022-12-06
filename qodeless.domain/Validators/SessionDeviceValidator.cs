using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace qodeless.domain.Validators
{
    public class SessionDeviceAddValidator : AbstractValidator<SessionDevice>
    {
        public SessionDeviceAddValidator()
        {
            RuleFor(entity => entity.UserPlayId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtEnd).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtEnd).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.LastIpAddress).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.LastIpAddress).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.LastIpAddress)
                .Matches(new Regex(@"^[A-F]|[0-9].(?=[\.])"))
                .WithMessage("Campo IP inválido");
        }
    }

    public class SessionDeviceUpdateValidator : AbstractValidator<SessionDevice>
    {
        public SessionDeviceUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserPlayId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtEnd).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtEnd).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.LastIpAddress).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.LastIpAddress).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.LastIpAddress)
                .Matches(new Regex(@"[A-F]|[0-9].(?=[\.])"))
                .WithMessage("Campo IP inválido");
        }
    }

    public class SessionDeviceRemoveValidator : AbstractValidator<SessionDevice>
    {
        public SessionDeviceRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
