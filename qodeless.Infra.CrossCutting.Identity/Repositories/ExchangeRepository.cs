using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class ExchangeRepository : Repository<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Exchange> GetExchanges()
        {
            return Db.Exchanges.Where(_ => _.Excluded == false);
        }

        public bool IsValidAccountId(Guid? accountId)
        {
            return Db.Accounts.Any(_ => _.Id == accountId);
        }
    }
}
