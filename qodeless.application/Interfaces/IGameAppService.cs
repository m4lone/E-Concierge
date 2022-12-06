using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IGameAppService : IDisposable
    {
        IEnumerable<GameViewModel> GetAll();
        GameViewModel GetById(Guid id);
        IEnumerable<GameViewModel> GetAllBy(Func<Game, bool> exp);
        IEnumerable<GameViewModel> GetAllIndex();
        ValidationResult Add(GameViewModel vm);
        ValidationResult Upsert(GameViewModel vm);
        ValidationResult Update(GameViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<GameViewModel> GetGames();
        IEnumerable<GameViewModel> GetGamesByAccountId(Guid id);
        public IEnumerable<ProfitRankingViewModel> GetRankingGames();
    }
}