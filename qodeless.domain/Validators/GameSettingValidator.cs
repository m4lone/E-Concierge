using FluentValidation;
using qodeless.domain.Entities;

namespace qodeless.domain.Validators
{
    public class GameSettingAddValidator : AbstractValidator<GameSetting>
    {
        public GameSettingAddValidator()
        {
            RuleFor(entity => entity.BallValue).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.BallValue).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class GameSettingUpdateValidator : AbstractValidator<GameSetting>
    {
        public GameSettingUpdateValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.BallValue).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.BallValue).NotNull().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotEmpty().WithMessage("Campo obrigatório");
            RuleFor(entity => entity.GameId).NotNull().WithMessage("Campo não pode ser nulo");
        }
    }

    public class GameSettingRemoveValidator : AbstractValidator<GameSetting>
    {
        public GameSettingRemoveValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
