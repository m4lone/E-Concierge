using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class OrderAddValidator : AbstractValidator<Order>
    {
        public OrderAddValidator()
        {
            RuleFor(entity => entity.Date).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Date).NotNull().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Value).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Value).NotNull().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.PixId).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.PixId).NotNull().WithMessage("Valor obrigatório");
        }
    }
    public class OrderUpdateValidator : AbstractValidator<Order>
    {
        public OrderUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Id).NotNull().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Date).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Date).NotNull().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Value).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.Value).NotNull().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.PixId).NotEmpty().WithMessage("Valor obrigatório");
            RuleFor(entity => entity.PixId).NotNull().WithMessage("Valor obrigatório");
        }
    }
    public class OrderRemoveValidator : AbstractValidator<Order>
    {
        public OrderRemoveValidator()
        {
        }
    }
}
