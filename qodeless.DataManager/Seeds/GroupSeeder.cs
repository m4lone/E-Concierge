using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;

namespace qodeless.DataManager.Seeds
{
    public class GroupSeeder
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region GROUPSEEDER
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var groupSeederId1 = Guid.Parse("92654b34-e56d-4ce1-b5c2-e10328097b72");
            var groupSeederId2 = Guid.Parse("4085bc16-b5e6-4509-b061-0ea00b4568ee");
            var items = new List<Group>() {
                new Group(groupSeederId1){ Code = "GP01",  Name = "GROUP 1", AcceptanceCriteria = 70},
                new Group(groupSeederId1){ Code = "GP02",  Name = "GROUP 2", AcceptanceCriteria = 55},
            };

            var repository = new GroupRepository(_dbContext);
            foreach (var item in items)
            {
                repository.Add(item,true);
            }
            #endregion //DEVICESEEDER

        }
    }
}
