using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace qodeless.domain.Validators
{
    public class VouscherAddValidator : AbstractValidator<Voucher>
    {
        public VouscherAddValidator(IVouscherRepository _vouscherRepository)
        {
            RuleFor(entity => entity.UserOperationId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.QrCodeKey).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.QrCodeKey).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteID).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteID).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.QrCodeKey)
                .Matches(new Regex(@"^RG[0-9]{4}(?:[A-Z0-9]{2})$"))
                .WithMessage("Código inválido");
        }
    }

    public class VouscherUpdateValidator : AbstractValidator<Voucher>
    {
        public VouscherUpdateValidator(IVouscherRepository _vouscherRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.QrCodeKey).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.QrCodeKey).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteID).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.SiteID).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Amount).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.QrCodeKey)
               .Matches(new Regex(@"^RG[0-9]{4}(?:[A-Z0-9]{2})$"))
               .WithMessage("Código inválido");
        }
    }

    public class VouscherRemoveValidator : AbstractValidator<Voucher>
    {
        public VouscherRemoveValidator(IVouscherRepository _vouscherRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
