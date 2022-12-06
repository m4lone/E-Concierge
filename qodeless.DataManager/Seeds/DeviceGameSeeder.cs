using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class DeviceGameSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            const int TOTAL_ITEMS = 200;
            int count = 0;
            var devices = new DeviceRepository(_dbContext).GetAll().ToList();
            var games = new GameRepository(_dbContext).GetAll().ToList();

            var devicesGame = new List<DeviceGame>();

            for (int i = 0; i < TOTAL_ITEMS; i++)
                devicesGame.Add(new DeviceGame(Guid.NewGuid()) { DeviceId = GetFKRandom(devices), GameId = GetFKRandom(games) });

            var deviceGameRepository = new DeviceGameRepository(_dbContext);
            foreach (var deviceGames in devicesGame)
            {
                count++;
                deviceGameRepository.Upsert(deviceGames, _ => _.DeviceId == deviceGames.DeviceId, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER
        }

    }
}