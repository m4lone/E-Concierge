using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System;

namespace qodeless.domain.Validators
{
    public class SessionSiteAddValidator : AbstractValidator<SessionSite>
    {
        public SessionSiteAddValidator()
        {
            RuleFor(entity => entity.UserOperationId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtEnd).NotNull().WithMessage("Data Inválida");
        }
    }

    public class SessionSiteUpdateValidator : AbstractValidator<SessionSite>
    {
        public SessionSiteUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtBegin).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtEnd).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DtEnd).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class SessionSiteRemoveValidator : AbstractValidator<SessionSite>
    {
        public SessionSiteRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
