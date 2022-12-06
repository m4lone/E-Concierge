using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class SiteGameRepository : Repository<SiteGame>, ISiteGameRepository
    {
        public SiteGameRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Game> GetGames()
        {
            return Db.Games;
        }

        public IQueryable<GameDropDownViewModel> GetGamesDrop()
        {
            return Db.Games.Select(x => new GameDropDownViewModel { Id = x.Id, Name = x.Name}).AsQueryable();
        }

        public IQueryable<Site> GetSites()
        {
            return Db.Sites;
        }

        public IQueryable<SiteDropDownViewModel> GetSitesDrop()
        {
            return Db.Sites.Select(x => new SiteDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }

        public bool IsValidGameId(Guid gameId)
        {
            return Db.Games.Any(c => c.Id == gameId);
        }

        public bool IsValidSiteId(Guid siteId)
        {
            return Db.Sites.Any(c => c.Id == siteId);
        }
    }
}
