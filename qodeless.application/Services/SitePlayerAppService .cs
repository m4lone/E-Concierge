using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class SitePlayerAppService : ISitePlayerAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ISitePlayerRepository _sitePlayerRepository;
        private readonly IPlayRepository _playRepository;
        public SitePlayerAppService(IMapper mapper, ISitePlayerRepository sitePlayerRepository, IPlayRepository playRepository)
        {
            _mapper = mapper;
            _sitePlayerRepository = sitePlayerRepository;
            _playRepository = playRepository;
    }

        public IEnumerable<SitePlayerViewModel> GetAll()
        {
            Logger.Info("== SitePlayer displayed on index ==");
            var user = _sitePlayerRepository.GetAllUsers();
            var site = _sitePlayerRepository.GetAll()
            .Include(_ => _.Site)
            .Select(_ => new SitePlayerViewModel
            {
                Id = _.Id,
                SiteId = _.SiteId,
                SiteName = _.Site.Name,
                UserPlayerId = _.UserPlayerId,
                Email = user.Where(x => x.Id == _.UserPlayerId).Select(x => x.Email).FirstOrDefault()
            }).AsNoTracking(); 
            return site;
        }
        public IEnumerable<Play> GetPlayersBySiteId(Guid siteId) 
        {
            return _playRepository.GetPlayersBySiteId(siteId);

        }

        public IEnumerable<SiteViewModel> GetSites()
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_sitePlayerRepository.GetSites());
        }

        public SitePlayerViewModel GetById(Guid id)
        {
            return _mapper.Map<SitePlayerViewModel>(_sitePlayerRepository.GetById(id));
        }

        public SitePlayerViewModel GetBySiteIdPlayerId(Guid siteId,string playerId)
        {
            return _mapper.Map<SitePlayerViewModel>(_sitePlayerRepository.GetBy(x => x.SiteId == siteId && x.UserPlayerId == playerId));
        }

        public IEnumerable<SitePlayerViewModel> GetAllBy(Func<SitePlayer, bool> exp)
        {
            return _mapper.Map<IEnumerable<SitePlayerViewModel>>(_sitePlayerRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing SitePlayer ==");
            var entity = _sitePlayerRepository.GetById(id);
            var validationResult = new SitePlayerRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _sitePlayerRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(SitePlayerViewModel vm)
        {
            Logger.Info("== Adding SitePlayer ==");
            var entity = _mapper.Map<SitePlayer>(vm);
            var validationResult = new SitePlayerAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _sitePlayerRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(SitePlayerViewModel vm)
        {
            Logger.Info("== Updating SitePlayer ==");
            var entity = _mapper.Map<SitePlayer>(vm);
            var validationResult = new SitePlayerUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _sitePlayerRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(SitePlayerViewModel vm)
        {
            Logger.Info("== Inserting SitePlayer ==");
            if (Guid.Empty == vm.Id)
            {
                return  Add(vm);
            }
            else
            {
                return  Update(vm);
            }
        }

        public IEnumerable<SiteDropDownViewModel> GetSitesDrop()
        {
            return _sitePlayerRepository.GetSitesDrop();
        }

        IEnumerable<SiteDropDownViewModel> ISitePlayerAppService.GetSitesDrop()
        {
            return _sitePlayerRepository.GetSitesDrop();
        }


        IEnumerable<UserViewModel> ISitePlayerAppService.GetUserDrop()
        {
            return _sitePlayerRepository.GetUserDrop();
        }

        public IEnumerable<SitePlayerViewModel> GetPlayer(Guid siteId)
        {
            return _mapper.Map<IEnumerable<SitePlayerViewModel>>(_sitePlayerRepository.GetAllBy(c => c.SiteId == siteId));
        }
    }
}