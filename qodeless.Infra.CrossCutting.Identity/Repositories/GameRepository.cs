using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Game> GetGamesByAccountId(Guid accountId)
        {
            var gamesByUser = Db
                .AccountGames
                .Where(x => x.AccountId == accountId)
                .Select(x => x.GameId)
                .ToList();
            return Db.Games.Where(x => gamesByUser.Any(a => a == x.Id));
        }
        public IQueryable<Game> GetGames()
        {
            return Db.Games;
        }
        public bool IsValidGameId(Guid gameId)
        {
            return Db.Games.Any(j => j.Id == gameId);
        }

        public IQueryable<ProfitRankingViewModel> ProfitRanking()
        {
            var result = new List<ProfitRankingViewModel>();
            var games = Db.Games.ToList();

            foreach (var game in games)
            {
                var plays = Db.Plays.ToList().Where(_ => _.GameId == game.Id).ToList().Sum(_=>_.AmountPlay);
                result.Add(new ProfitRankingViewModel { Game = game.Name, Profit = plays });
            }

            return result.OrderByDescending(_ => _.Profit).AsQueryable();

        }
    }
}
