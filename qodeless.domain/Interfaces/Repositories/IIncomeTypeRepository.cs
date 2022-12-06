using qodeless.domain.Entities;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IIncomeTypeRepository : IRepository<IncomeType> 
    {
        IQueryable<IncomeType> GetIncomeTypes();
    }
}
