using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class ExpenseSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var account = new AccountRepository(_dbContext).GetAll().ToList();
            int count = 0;
            #region EXPENSESEEDER
            var expenseSeeds = new List<Expense>() {
                new Expense(Guid.NewGuid()){SiteId = GetFKRandom(sites), Amount=300.0, AccountId = GetFKRandom(account),Description = "Jogador venceu!", Type = EIncomeType.Play},
                new Expense(Guid.NewGuid()){SiteId = GetFKRandom(sites), Amount=3000.0, AccountId = GetFKRandom(account),Description = "Pagamento de fatura!", Type = EIncomeType.Billing}
            };

            var ExpenseRepository = new ExpenseRepository(_dbContext);
            foreach (var expenseSeed in expenseSeeds)
            {
                count++;
                ExpenseRepository.Upsert(expenseSeed, _ => _.Id == expenseSeed.Id, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //EXPENSESEEDER

        }
    }
}

