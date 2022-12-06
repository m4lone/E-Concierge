using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class IncomeTypeRepository : Repository<IncomeType>, IIncomeTypeRepository
    {
        public IncomeTypeRepository(ApplicationDbContext context) : base(context)
        {
        
        }

        public IQueryable<IncomeType> GetIncomeTypes()
        {
            return Db.IncomeTypes;
        }
    }
}
