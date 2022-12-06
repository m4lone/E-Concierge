using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class SitePlayerRepository : Repository<SitePlayer>, ISitePlayerRepository
    {
        public SitePlayerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Site> GetSites()
        {
            return Db.Sites;
        }

        public IQueryable<SiteDropDownViewModel> GetSitesDrop()
        {
            return Db.Sites.Select(x => new SiteDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }

        public IQueryable<UserViewModel> GetUserDrop()
        {
            return Db.Users.Select(x => new UserViewModel { Id = x.Id, Email = x.Email }).AsQueryable();
        }
    }
}