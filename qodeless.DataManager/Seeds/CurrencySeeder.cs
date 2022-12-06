using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;

namespace qodeless.DataManager.Seeds
{
    public class CurrencySeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region CURRENCYSEEDER
            int count = 0;
            var currencySeeds = new List<Currency>() {
                new Currency(Guid.NewGuid()){Code = ECurrencyCode.EUR, VlrToBRL = 7.88 },
                new Currency(Guid.NewGuid()){Code = ECurrencyCode.USD, VlrToBRL = 5.51 },
                new Currency(Guid.NewGuid()){Code = ECurrencyCode.BRL, VlrToBRL = 1 }
            };

            var CurrencyRepository = new CurrencyRepository(_dbContext);
            foreach (var currencySeed in currencySeeds)
            {
                count++;
                CurrencyRepository.Upsert(currencySeed, _ => _.Code == currencySeed.Code, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //CURRENCYSEEDER

        }
    }
}

