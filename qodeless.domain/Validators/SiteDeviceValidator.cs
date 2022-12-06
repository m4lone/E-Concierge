using FluentValidation;
using qodeless.domain.Entities;
using System;

namespace qodeless.domain.Validators
{
    public class SiteDeviceAddValidator : AbstractValidator<SiteDevice>
    {
        public SiteDeviceAddValidator()
        {           
            RuleFor(entity => entity.SiteId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteId).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.DeviceId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DeviceId).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class SiteDeviceUpdateValidator : AbstractValidator<SiteDevice>
    {
        public SiteDeviceUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteId).NotNull().WithMessage("Campo não pode ser nulo");
            RuleFor(entity => entity.DeviceId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DeviceId).NotNull().WithMessage("Campo não pode ser nulo");
        }

        public class SiteDeviceRemoveValidator : AbstractValidator<SiteDevice>
        {
            public SiteDeviceRemoveValidator()
            {
                RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            }
        }
    }
}
