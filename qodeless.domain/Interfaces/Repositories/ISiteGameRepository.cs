using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ISiteGameRepository : IRepository<SiteGame> //SOLID
    {
        IQueryable<GameDropDownViewModel> GetGamesDrop();
        IQueryable<SiteDropDownViewModel> GetSitesDrop();
        IQueryable<Site> GetSites();
        bool IsValidSiteId(Guid siteId);
        IQueryable<Game> GetGames();
        bool IsValidGameId(Guid gameId);
    }
}
