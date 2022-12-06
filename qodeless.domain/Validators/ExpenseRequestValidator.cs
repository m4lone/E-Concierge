using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class ExpenseRequestAddValidator : AbstractValidator<ExpenseRequest>
    {
        public ExpenseRequestAddValidator()
        {
            RuleFor(entity => entity.UserOperationID).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationID).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Request).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Request).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class ExpenseRequestUpdateValidator : AbstractValidator<ExpenseRequest>
    {
        public ExpenseRequestUpdateValidator()
        {
            RuleFor(entity => entity.UserOperationID).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.UserOperationID).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.DueDate).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Request).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.Request).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class ExpenseRequestRemoveValidator : AbstractValidator<ExpenseRequest>
    {
        public ExpenseRequestRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
