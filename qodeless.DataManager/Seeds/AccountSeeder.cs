using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;

namespace qodeless.DataManager.Seeds
{
    public class AccountSeeder
    {
        public static Guid accountId1 = Guid.Parse("45597340-4047-43cd-905a-de03bb55862c");
        public static Guid accountId2 = Guid.Parse("b4d2bdab-5870-47f3-81f0-ca1180c7364c");
        public static void Seed(ApplicationDbContext _dbContext)
        {
            int count = 0;
            #region ACCOUNT
            //MOCK dos dados, ou seja, Dados Fakes
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx

            var accounts = new List<Account>() {
                new Account(accountId1){ Name = "RG Digital", Description = "RG Digital", Status = EAccountStatus.Actived, Royalties = 10},
                new Account(accountId2){ Name = "Parceiro", Description = "Parceiro", Status = EAccountStatus.Actived,Royalties = 10},
            };

            var accountRepository = new AccountRepository(_dbContext);
            foreach (var account in accounts)
            {
                count++;
                accountRepository.Upsert(account, _ => _.Id == account.Id, true);
            }
            #endregion //ACCOUNT
            Console.WriteLine($"Itens salvos -> {count}");
        }
    }
}
