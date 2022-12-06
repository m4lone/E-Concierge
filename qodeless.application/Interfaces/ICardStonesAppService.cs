using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;

namespace qodeless.application
{
    public interface ICardStonesAppService : IDisposable
    {
        IEnumerable<CardStonesViewModel> GetAll();
        CardStonesViewModel GetById(Guid id);
        IEnumerable<CardStonesViewModel> GetAllBy(Func<CardStones, bool> exp);
        CardStonesViewModel GetBy(Func<CardStones, bool> exp);
        ValidationResult Add(CardStonesViewModel vm);
        ValidationResult Update(CardStonesViewModel vm);
        ValidationResult Remove(Guid id);
        string GenerateCardNumber(CardStonesAddViewModel vm);
    }
}
