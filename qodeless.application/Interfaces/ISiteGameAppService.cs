using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application.Interfaces
{
    public interface ISiteGameAppService : IDisposable
    {
        IEnumerable<SiteGameViewModel> GetAll();
        SiteGameViewModel GetById(Guid id);
        IEnumerable<SiteGameViewModel> GetAllBy(Func<SiteGame, bool> exp);
        ValidationResult Add(SiteGameViewModel vm);
        ValidationResult Update(SiteGameViewModel vm);
        ValidationResult Upsert(SiteGameViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<SiteViewModel> GetSites();
        IEnumerable<GameViewModel> GetGames();
        IEnumerable<GameDropDownViewModel> GetGamesDrop();
        IEnumerable<SiteDropDownViewModel> GetSitesDrop();
        IEnumerable<SiteGameViewModel> GetSiteGame(Guid siteId);
        ValidationResult UpdateGameBySiteId(SiteGameMutipleVm siteGameMutipleVm);
    }
}
