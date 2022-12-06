using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;

namespace qodeless.DataManager.Seeds
{
    public class IncomeTypeSeeder
    { 
        public static Guid incometypeId1 = Guid.Parse("09b36bed-1250-437c-8779-944263675903");
        public static Guid incometypeId2 = Guid.Parse("38f60497-d881-4089-a43e-e70c1052ec29");
        public static void Seed(ApplicationDbContext _dbContext)
        {

            #region INCOMETYPESEEDER
            int count = 0;
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var incometypeId1 = Guid.Parse("09b36bed-1250-437c-8779-944263675903");
            var incometypeId2 = Guid.Parse("38f60497-d881-4089-a43e-e70c1052ec29");
            var incometypes = new List<IncomeType>() {
            new IncomeType(incometypeId1){Name = "Benfeitorias", Code = 50},
            new IncomeType(incometypeId2){Name = "Auxilio", Code = 40 },
            };

            var IncomeTypeRepository = new IncomeTypeRepository(_dbContext);
            foreach (var incometype in incometypes)
            {
                count++;
                IncomeTypeRepository.Upsert(incometype, _ => _.Id == incometype.Id, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //INCOMESEEDER

        }
    }
}

