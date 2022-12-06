using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class AccountGameSeeder : SeederBase
    {


        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            const int TOTAL_ITEMS = 50;
            int count = 0;

            var accounts = new AccountRepository(_dbContext).GetAll().ToList();
            var games = new GameRepository(_dbContext).GetAll().ToList();

            var accountsGame = new List<AccountGame>();

            for (int i = 0; i < TOTAL_ITEMS; i++)
                accountsGame.Add(new AccountGame(Guid.NewGuid()) { AccountId = GetFKRandom(accounts), GameId = GetFKRandom(games) });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                accountsGame.Add(new AccountGame(Guid.NewGuid()) { AccountId = GetFKRandom(accounts), GameId = GetFKRandom(games) });

            var accountGameRepository = new AccountGameRepository(_dbContext);
            foreach (var accountGames in accountsGame)
            {
                count++;
                accountGameRepository.Upsert(accountGames, _ => _.AccountId == accountGames.AccountId && _.GameId == accountGames.GameId, true);
            }
            #endregion //DEVICESEEDER
            Console.WriteLine($"Itens salvos -> {count}");
        }

    }
}