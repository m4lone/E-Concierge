using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System;

namespace qodeless.domain.Validators
{
    public class SuccessFeeAddValidator : AbstractValidator<SuccessFee>
    {
        public SuccessFeeAddValidator()
        {
            RuleFor(entity => entity.UserId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Rate).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Rate).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");            
        }
    }

    public class SuccessFeeUpdateValidator : AbstractValidator<SuccessFee>
    {
        public SuccessFeeUpdateValidator()
        {
            const double MAX_RATE_BY_ACCOUNT = 50.0;
            const double MAX_RATE_BY_SITE = 50.0;
            const double TOTAL_RATE = 100.0;

            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserId).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Rate).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Rate).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Type).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Rate)
                .LessThanOrEqualTo(MAX_RATE_BY_ACCOUNT)
                .When(c=>c.Type == Enums.EFeeType.ByAccount)
                .WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Rate)
                .LessThanOrEqualTo(MAX_RATE_BY_SITE)
                .When(c => c.Type == Enums.EFeeType.BySite)
                .WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Rate)
                .LessThan(TOTAL_RATE)
                .When(c => c.Type == Enums.EFeeType.ByParnetUser)
                .WithMessage("Campo obrigatório");
        }
    }

    public class SuccessFeeRemoveValidator : AbstractValidator<SuccessFee>
    {
        public SuccessFeeRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
