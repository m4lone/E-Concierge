using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IAccountGameAppService : IDisposable
    {
        IEnumerable<AccountGameViewModel> GetAll();
        AccountGameViewModel GetById(Guid id);
        IEnumerable<AccountGameViewModel> GetAllBy(Func<AccountGame, bool> exp);
        ValidationResult Add(AccountGameViewModel vm);
        ValidationResult Update(AccountGameViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<Game> GetGamesByAccountId(Guid accountId);
        ValidationResult Upsert(AccountGameViewModel vm);
        ValidationResult UpdateGamesByAccountId(AccountGameMutiplevm accountGameMutiplevm);
        IEnumerable<AccountDropDownViewModel> GetAccountsDrop();
        IEnumerable<GameDropDownViewModel> GetGamesDrop();

    }
}