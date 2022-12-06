using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class SitePlayerSeeder : SeederBase
    {

        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region SITEPLAYERSEEDER
            const int TOTAL_ITEMS = 50;
            int count = 0;
            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var users = _dbContext.Users.ToList();
            var sitePlayers = new List<SitePlayer>();

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sitePlayers.Add(new SitePlayer(Guid.NewGuid()) { SiteId = GetFKRandom(sites), UserPlayerId= GetFKRandomUser(users) });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sitePlayers.Add(new SitePlayer(Guid.NewGuid()) { SiteId = GetFKRandom(sites), UserPlayerId = GetFKRandomUser(users) });

            var sitePlayerRepository = new SitePlayerRepository(_dbContext);
            foreach (var sitePlayerGames in sitePlayers)
            {
                count++;
                sitePlayerRepository.Upsert(sitePlayerGames, _ => _.SiteId == sitePlayerGames.SiteId, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //SITEPLAYERSEEDER

        }
    }
}
