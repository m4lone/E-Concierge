using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class IncomeSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region INCOMESEEDER
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var incomeSeederId1 = Guid.Parse("ca5ae203-b4ea-49d3-b8ed-2c8cebdb880f");
            var incomeSeederId2 = Guid.Parse("1b96042c-9b8e-4813-821b-4fc4599482b2");
            var users = _dbContext.Users.ToList();
            int count = 0;
            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var incomeSeeds = new List<Income>() {
                new Income(incomeSeederId1){SiteId = GetFKRandom(sites), AccountId = AccountSeeder.accountId1,  Description = "Cassino RG 1", Type = EIncomeType.Play, Amount = 769, },
                new Income(incomeSeederId2){SiteId = GetFKRandom(sites), AccountId = AccountSeeder.accountId2, Description = "Cassino RG 2", Type = EIncomeType.SuccessFee, Amount = 987,  }
            };

            var IncomeRepository = new IncomeRepository(_dbContext);
            foreach (var incomeSeed in incomeSeeds)
            {
                count++;
                IncomeRepository.Upsert(incomeSeed, _ => _.Id == incomeSeed.Id, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //INCOMESEEDER

        }
    }
}

