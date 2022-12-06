using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    [UbuntuSeeder]
    public class GameSeeder
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            int count = 0;
            var gameSeeds = new List<Game>();
            foreach (var enumCode in Enum.GetValues(typeof(EGameCode)).Cast<EGameCode>().ToList())
            {
                gameSeeds.Add(new Game(Guid.NewGuid()) { Code = enumCode, Name = enumCode.ToString(), DtPublish = DateTime.Now, Status = EGameStatus.Actived, Version = "V1.1", Type = EGameType.Bingo });
            };

            var gameRepository = new GameRepository(_dbContext);
            foreach (var gameSeed in gameSeeds)
            {
                count++;
                gameRepository.Upsert(gameSeed, _ => _.Code == gameSeed.Code, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER

        }
    }
}
