using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IGameRepository : IRepository<Game> //SOLID
    {
        IQueryable<Game> GetGamesByAccountId(Guid accountId);
        IQueryable<Game> GetGames();
        bool IsValidGameId(Guid gameId);
        public IQueryable<ProfitRankingViewModel> ProfitRanking();
    }
}