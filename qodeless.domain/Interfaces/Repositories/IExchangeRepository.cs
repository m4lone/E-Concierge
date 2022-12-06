using qodeless.domain.Entities;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IExchangeRepository : IRepository<Exchange> 
    {
        IQueryable<Exchange> GetExchanges();
        bool IsValidAccountId(Guid? accountId);
    }
}
