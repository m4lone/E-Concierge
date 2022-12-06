using AutoMapper;
using FluentValidation.Results;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static qodeless.domain.Validators.SiteGameUpdateValidator;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using qodeless.domain.Model;
using NLog;
using qodeless.domain.Enums;

namespace qodeless.application.Services
{

    public class SiteGameAppService : ISiteGameAppService
    {

        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ISiteGameRepository _siteGameRepository;
        public SiteGameAppService(IMapper mapper, ISiteGameRepository SiteGameRepository)
        {
            _mapper = mapper;
            _siteGameRepository = SiteGameRepository;
        }

        public IEnumerable<SiteGameViewModel> GetAll()
        {
           Logger.Info("== SiteGame displayed on the index ==");
            var siteGames = _siteGameRepository.GetAll()
                .Include(_ => _.Site).ThenInclude(c=>c.Account)
                .Include(_ => _.Game)
                .Select(_ => new SiteGameViewModel
                {
                    Id = _.Id,
                    SiteId = _.SiteId,
                    SiteName = _.Site.Name,     
                    GameId = _.Game.Id,
                    GameName = _.Game.Name,
                    ESite = _.ESite
                }).AsNoTracking();
            Dispose();
            return siteGames;
        }

        public SiteGameViewModel GetById(Guid id)
        {
            return _mapper.Map<SiteGameViewModel>(_siteGameRepository.GetById(id));
        }
        public IEnumerable<SiteViewModel> GetSites()
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_siteGameRepository.GetSites());
        }
        public IEnumerable<GameViewModel> GetGames()
        {
            return _mapper.Map<IEnumerable<GameViewModel>>(_siteGameRepository.GetGames());
        }
        public IEnumerable<SiteGameViewModel> GetAllBy(Func<SiteGame, bool> exp)
        {
            return _mapper.Map<IEnumerable<SiteGameViewModel>>(_siteGameRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
           Logger.Info("== Removing site game ==");
            var entity = _siteGameRepository.GetById(id);
            var validationResult = new SiteGameRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _siteGameRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(SiteGameViewModel vm)
        {
           Logger.Info("== Adding site game ==");
            var entity = _mapper.Map<SiteGame>(vm);
            var validationResult = new SiteGameAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _siteGameRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(SiteGameViewModel vm)
        {
            Logger.Info("== Updating site game ==");
            var entity = _mapper.Map<SiteGame>(vm);
            var validationResult = new SiteGameUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _siteGameRepository.Update(entity);

            return validationResult;
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(SiteGameViewModel vm)
        {
            Logger.Info("== Inserting site game ==");
            if (Guid.Empty == vm.Id)
            {
                return  Add(vm);
            }
            else
            {
                return  Update(vm);
            }
        }
        public IEnumerable<GameDropDownViewModel> GetGamesDrop()
        {
            return _siteGameRepository.GetGamesDrop();
        }

        public IEnumerable<SiteDropDownViewModel> GetSitesDrop()
        {
            return _siteGameRepository.GetSitesDrop();
        }
        public IEnumerable<SiteGameViewModel> GetSiteGame(Guid siteId)
        {
            return _mapper.Map<IEnumerable<SiteGameViewModel>>(_siteGameRepository.GetAllBy(c => c.SiteId == siteId));
        }

        public ValidationResult UpdateGameBySiteId(SiteGameMutipleVm siteGameMutipleVm)
        {
            Logger.Info("== Updating games by account ==");
            ValidationResult validationResult = null;
            var siteGames = GetAllBy(x => x.SiteId == siteGameMutipleVm.Site);
            if (siteGames != null)
            {
                foreach (var siteGame in siteGames)
                {
                    Remove(siteGame.Id);
                }
            }

            foreach (var gameId in siteGameMutipleVm.GameIds)
            {
                var entity = new SiteGame(siteGameMutipleVm.Site, gameId,ESite.Available);
                validationResult = new SiteGameAddValidator().Validate(entity);
                if (validationResult.IsValid)
                    _siteGameRepository.Add(entity);
            }
            return validationResult;
        }
    }
}
