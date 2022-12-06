using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class SessionSiteRepository : Repository<SessionSite>, ISessionSiteRepository
    {
        public SessionSiteRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Site> GetSites()
        {
            return Db.Sites;
        }
    }
}
