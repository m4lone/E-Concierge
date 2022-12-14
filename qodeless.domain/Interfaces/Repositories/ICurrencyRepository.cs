using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ICurrencyRepository : IRepository<Currency> 
    {
        public IQueryable<Currency> GetCurrencys();
    }
}
