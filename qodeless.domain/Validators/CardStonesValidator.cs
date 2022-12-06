using FluentValidation;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace qodeless.domain.Validators
{
    public class CardStonesAddValidator : AbstractValidator<CardStones>
    {
        public CardStonesAddValidator(ICardStonesRepository cardStonesRepository)
        {
            RuleFor(entity => entity.CardNumber).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.CardNumber).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.StoneNumbersList).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.StoneNumbersList).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class CardStonesUpdateValidator : AbstractValidator<CardStones>
    {
        public CardStonesUpdateValidator(ICardStonesRepository cardStonesRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.CardNumber).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.CardNumber).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.StoneNumbersList).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.StoneNumbersList).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo obrigatório");
        }
    }

    public class CardStonesRemoveValidator : AbstractValidator<CardStones>
    {
        public CardStonesRemoveValidator(ICardStonesRepository cardStonesRepository)
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
