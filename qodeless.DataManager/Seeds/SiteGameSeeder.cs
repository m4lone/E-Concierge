using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class SiteGameSeeder : SeederBase
    {

        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            const int TOTAL_ITEMS = 20;
            int count = 0;
            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var games = new GameRepository(_dbContext).GetAll().ToList();

            var siteGames = new List<SiteGame>();
            foreach (var site in sites)
            {
                for (int i = 0; i < TOTAL_ITEMS; i++)
                    siteGames.Add(new SiteGame(Guid.NewGuid()) { SiteId = site.Id, GameId = GetFKRandom(games), ESite = ESite.Available });
            }

            var siteGameRepository = new SiteGameRepository(_dbContext);
            var accountGameRepository = new AccountGameRepository(_dbContext);
            var deviceGameRepository = new DeviceGameRepository(_dbContext);
            foreach (var siteGame in siteGames)
            {
                if (siteGameRepository.GetAllBy(_ => _.SiteId == siteGame.SiteId).Count() >= 20) //Max 20 randomic items
                    continue;

                if (!siteGameRepository.Any(_=>_.SiteId == siteGame.SiteId && _.GameId == siteGame.GameId))
                {
                    siteGameRepository.Add(siteGame,true);
                    count++;
                    var accountId = sites.FirstOrDefault(_ => _.Id == siteGame.SiteId).AccountId;
                    var acctGame = new AccountGame(Guid.NewGuid()) { AccountId = accountId, GameId = siteGame.GameId };
                    accountGameRepository.Upsert(acctGame, _=>_.AccountId == acctGame.AccountId && _.GameId == acctGame.GameId, true);
                }
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER

        }
    }
}
