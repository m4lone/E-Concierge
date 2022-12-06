using qodeless.domain.Entities;
using qodeless.domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ISitePlayerRepository : IRepository<SitePlayer> //SOLID
    {
        IQueryable<Site> GetSites();
        IQueryable<SiteDropDownViewModel> GetSitesDrop();
        IQueryable<UserViewModel> GetUserDrop();
    }
}