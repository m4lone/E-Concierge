using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;

namespace qodeless.DataManager.Seeds
{
    public class SiteSeeder
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region SITE
            var sites = new List<Site>() {
                new Site(Guid.NewGuid()){Code = "RG5847AH", Name = "Bar do Barba", AccountId = AccountSeeder.accountId1, Description = "Bar do Barba", ESiteType = ESiteType.Default},
                new Site(Guid.NewGuid()){Code = "RG6822AG", Name = "Bar da Torres", AccountId = AccountSeeder.accountId1,Description = "Bar da Torres", ESiteType = ESiteType.Default},
                new Site(Guid.NewGuid()){Code = "RG0847AR", Name = "Bar do ED", AccountId =  AccountSeeder.accountId2, Description = "Bar do ED", ESiteType = ESiteType.Casino},
                new Site(Guid.NewGuid()){Code = "RG3644AY", Name = "Lan House Barba", AccountId =  AccountSeeder.accountId2, Description = "Lan House Barba", ESiteType = ESiteType.LanHouse},
            };
            int count = 0;
            var siteRepository = new SiteRepository(_dbContext);
            foreach (var site in sites)
            {
                count++;
                site.Address = "Av Paulista, 123";
                site.City = "Sao Paulo";
                site.Country = "Brasil";
                site.ZipCode = "08032-123";
                siteRepository.Upsert(site, _ => _.Name == site.Name, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //SITE
        }
    }
}
