using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ISessionSiteRepository : IRepository<SessionSite> //SOLID
    {
        IQueryable<Site> GetSites();
    }
}
