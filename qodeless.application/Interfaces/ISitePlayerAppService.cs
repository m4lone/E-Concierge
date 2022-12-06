using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface ISitePlayerAppService : IDisposable
    {
        IEnumerable<SitePlayerViewModel> GetAll();
        SitePlayerViewModel GetById(Guid id);
        IEnumerable<SitePlayerViewModel> GetAllBy(Func<SitePlayer, bool> exp);
        ValidationResult Add(SitePlayerViewModel vm);
        ValidationResult Update(SitePlayerViewModel vm);
        ValidationResult Remove(Guid id);
        ValidationResult Upsert(SitePlayerViewModel vm);
        IEnumerable<SiteViewModel> GetSites();
        IEnumerable<SiteDropDownViewModel> GetSitesDrop();
        IEnumerable<UserViewModel> GetUserDrop();
        IEnumerable<Play> GetPlayersBySiteId(Guid siteId);
        IEnumerable<SitePlayerViewModel> GetPlayer(Guid siteId);
        SitePlayerViewModel GetBySiteIdPlayerId(Guid siteId, string playerId);

    }
}