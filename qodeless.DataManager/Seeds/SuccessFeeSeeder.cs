using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class SuccessFeeSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {


            #region SUCCESSFEE
            int count = 0;
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var successFeeId1 = Guid.Parse("da06bf4d-c63a-49c8-9137-391c313e9fc1");
            var successFeeId2 = Guid.Parse("b5d697af-3e4f-4b76-99a0-bdcfa796748f");
            var successFeeId3 = Guid.Parse("2427ee49-c1e1-47c1-ac5c-05d84b2ca6a2");
            var successFeeId4 = Guid.Parse("e3d77728-7b50-4da6-9050-331256f35abf");

            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var users = _dbContext.Users.ToList();

            var successFees = new List<SuccessFee>() {
                new SuccessFee(successFeeId1){ AccountId = AccountSeeder.accountId1, SiteId = GetFKRandom(sites), UserId = GetFKRandomUser(users), Rate = 18, Type = EFeeType.ByAccount },
                new SuccessFee(successFeeId2){ AccountId = AccountSeeder.accountId2, SiteId = GetFKRandom(sites), UserId = GetFKRandomUser(users), Rate = 18, Type = EFeeType.ByAccount},
                new SuccessFee(successFeeId3){ AccountId = AccountSeeder.accountId1, SiteId = GetFKRandom(sites), UserId = GetFKRandomUser(users), Rate = 18, Type = EFeeType.BySite},
                new SuccessFee(successFeeId4){ AccountId = AccountSeeder.accountId2, SiteId = GetFKRandom(sites), UserId = GetFKRandomUser(users), Rate = 18, Type = EFeeType.BySite},
            };

            var successFeeRepository = new SuccessFeeRepository(_dbContext);
            foreach (var successFee in successFees)
            {
                count++;
                successFeeRepository.Upsert(successFee, _ => _.Id == successFee.Id, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //SUCCESSFEE

        }
    }
}
