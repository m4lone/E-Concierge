using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class VouscherSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            var vouscherSeederId1 = Guid.Parse("53ae6156-4787-4bb2-bfb0-4c02d497fdd6");
            var vouscherSeederId2 = Guid.Parse("a8df2a1f-6cc4-4e53-a03e-c3a2024c3d8a");
            int count = 0;
            var users = _dbContext.Users.ToList();
            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var vouscherSeeds = new List<Voucher>() {
                new Voucher(vouscherSeederId1){  UserOperationId = GetFKRandomUser(users), QrCodeKey = "724", QrCodeSecret = "123", DueDate = DateTime.Now, Amount = 1500000, SiteID = GetFKRandom(sites)},
                new Voucher(vouscherSeederId2){  UserOperationId = GetFKRandomUser(users), QrCodeKey = "850", QrCodeSecret = "321", DueDate = DateTime.Now, Amount = 1100000, SiteID = GetFKRandom(sites)},
            };

            var VoscherRepository = new VouscherRepository(_dbContext);
            foreach (var vouscherSeed in vouscherSeeds)
            {
                count++;
                VoscherRepository.Upsert(vouscherSeed, _ => _.Id == vouscherSeed.Id, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER

        }
    }
}
