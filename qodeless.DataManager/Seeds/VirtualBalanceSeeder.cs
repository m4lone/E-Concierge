using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class VirtualBalanceSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region VirtualBalanceSEEDER
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var virtualBalanceSeederId1 = Guid.Parse("ec904382-166b-4920-8f9d-81538de112f1");
            var virtualBalanceSeederId2 = Guid.Parse("791b9d7b-f5cb-414f-bd47-07a4e87af1ef");
            int count = 0;
            var users = _dbContext.Users.ToList();
            var items = new List<VirtualBalance>() {
                new VirtualBalance(virtualBalanceSeederId1){ UserPlayId = GetFKRandomUser(users), UserOperationId = GetFKRandomUser(users), Type= EBalanceType.Credit,Amount=500, Description="gambler"},
                new VirtualBalance(virtualBalanceSeederId2){ UserPlayId = GetFKRandomUser(users), UserOperationId = GetFKRandomUser(users), Type= EBalanceType.Debit,Amount=100, Description="gambler"},
            };

            var repository = new VirtualBalanceRepository(_dbContext);
            foreach (var item in items)
            {
                count++;
                repository.Add(item,true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //VIRTUALBALANCESEEDER

        }
    }
}
