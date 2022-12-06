using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class PlaySeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            const int TOTAL_ITEMS = 50;
            int count = 0;
            var sessiondevices = new SessionDeviceRepository(_dbContext).GetAll().ToList();
            var accounts = new AccountRepository(_dbContext).GetAll().ToList();
            var games = new GameRepository(_dbContext).GetAll().ToList();
            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var users = _dbContext.Users.ToList();
            var plays = new List<Play>();

            for (int i = 0; i < TOTAL_ITEMS; i++)
                plays.Add(new Play(Guid.NewGuid()) { SessionDeviceId = GetFKRandom(sessiondevices), SiteId = GetFKRandom(sites), GameId = GetFKRandom(games), AmountPlay = 30, AmountExtraball = 12, UserPlayId = GetFKRandomUser(users) });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                plays.Add(new Play(Guid.NewGuid()) { SessionDeviceId = GetFKRandom(sessiondevices), SiteId = GetFKRandom(sites), GameId = GetFKRandom(games), AmountPlay = 30, AmountExtraball = 19, UserPlayId = GetFKRandomUser(users) });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                plays.Add(new Play(Guid.NewGuid()) { SessionDeviceId = GetFKRandom(sessiondevices), SiteId = GetFKRandom(sites), GameId = GetFKRandom(games), AmountPlay = 30, AmountExtraball = 5, UserPlayId = GetFKRandomUser(users) });

            var playRepository = new PlayRepository(_dbContext);
            foreach (var play in plays)
            {
                count++;
                playRepository.Upsert(play, _ => _.SessionDeviceId == play.SessionDeviceId && _.SiteId == play.SiteId && _.GameId == play.GameId, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER

        }

    }
}