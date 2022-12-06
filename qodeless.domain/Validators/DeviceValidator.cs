using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace qodeless.domain.Validators
{
    public class DeviceAddValidator : AbstractValidator<Device>
    {
        public DeviceAddValidator()
        {
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SerialNumber).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SerialNumber).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.MacAddress).NotEmpty().WithMessage("Campo obrigatório1");
            RuleFor(entity => entity.MacAddress).NotNull().WithMessage("Campo obrigatório2");
            RuleFor(entity => entity.MacAddress)
                .Matches(new Regex(@"^[a-fA-F0-9]{2}(:[a-fA-F0-9]{2}){5}$"))
                .WithMessage("MacAddressInválido");
        }

        public bool IsValidMacAddress(string macAddress)
        {
            return false;
        }
    }

    public class DeviceUpdateValidator : AbstractValidator<Device>
    {
        public DeviceUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Code).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SerialNumber).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SerialNumber).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.MacAddress).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.MacAddress).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.MacAddress)
                .Matches(new Regex(@"^[a-fA-F0-9]{2}(:[a-fA-F0-9]{2}){5}$"))
                .WithMessage("MacAddressInválido");

        }
    }

    public class DeviceRemoveValidator : AbstractValidator<Device>
    {
        public DeviceRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
