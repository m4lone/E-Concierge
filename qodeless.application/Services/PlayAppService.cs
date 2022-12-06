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
    public class PlayAppService : IPlayAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IPlayRepository _PlayRepository;
        public PlayAppService(IMapper mapper, IPlayRepository PlayRepository)
        {
            _mapper = mapper;
            _PlayRepository = PlayRepository;
        }

        public IEnumerable<PlayViewModel> GetAll()
        {
            Logger.Info("== Data (Play) that's displayed on index ==");
            var user = _PlayRepository.GetAllUsers();
            var play = _PlayRepository.GetAll()
            .Include(_ => _.SessionDevice).ThenInclude(_ => _.Device)
            .Include(_ => _.Game)
            .Include(_ => _.Site)
            .Select(_ => new PlayViewModel
            {
                Id = _.Id,
                AmountPlay = _.AmountPlay,
                AmountExtraball = _.AmountExtraball,
                DeviceCode = _.Device.Code,
                GameName = _.Game.Name,
                SiteName = _.Site.Name,
                LastIpAdress = _.SessionDevice.LastIpAddress,
                Email = user.Where(x => x.Id == _.UserPlayId).Select(x => x.Email).FirstOrDefault()
            });
            return play;
        }

        public PlayViewModel GetById(Guid id)
        {
            return _mapper.Map<PlayViewModel>(_PlayRepository.GetById(id));
        }

        public IEnumerable<PlayViewModel> GetBySiteId(Guid id, string userId)
        {
            Logger.Info("== Data (Play) that's displayed on index ==");

            var user = _PlayRepository.GetAllUsers();
            var play = _PlayRepository.GetAll()
            .Include(_ => _.SessionDevice).ThenInclude(_ => _.Device)
            .Include(_ => _.Game)
            .Include(_ => _.Site)
            .Where(x => x.SiteId == id)
            .Select(_ => new PlayViewModel
            {
                Id = _.Id,
                AmountPlay = _.AmountPlay,
                AmountExtraball = _.AmountExtraball,
                DeviceCode = _.Device.Code,
                GameName = _.Game.Name,
                SiteName = _.Site.Name,
                LastIpAdress = _.SessionDevice.LastIpAddress,
                Email = user.Where(x => x.Id == userId).Select(x => x.NickName).FirstOrDefault()
            });
            return play;
        }

        public IEnumerable<PlayViewModel> GetAllBy(Func<Play, bool> exp)
        {
            return _mapper.Map<IEnumerable<PlayViewModel>>(_PlayRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
           Logger.Info("== Removing data (Play) ==");
            var entity = _PlayRepository.GetById(id);
            var validationResult = new PlayRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _PlayRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(PlayViewModel vm)
        {
           Logger.Info("== Adding data (Play) ==");
            var entity = _mapper.Map<Play>(vm);
            var validationResult = new PlayAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _PlayRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(PlayViewModel vm)
        {

            Logger.Info("== Updating data (Play) ==");
            var entity = _mapper.Map<Play>(vm);
            var validationResult = new PlayUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _PlayRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public IEnumerable<DeviceViewModel> GetDevices()
        {
            return _mapper.Map<IEnumerable<DeviceViewModel>>(_PlayRepository.GetDevices());
        }

        public IEnumerable<SessionDeviceViewModel> GetSessionDevices()
        {
            return _mapper.Map<IEnumerable<SessionDeviceViewModel>>(_PlayRepository.GetSessionDevices());
        }

        public IEnumerable<GameViewModel> GetGames()
        {
            return _mapper.Map<IEnumerable<GameViewModel>>(_PlayRepository.GetGames());
        }

        public IEnumerable<AccountViewModel> GetAccounts()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(_PlayRepository.GetAccounts());
        }

        public ValidationResult Upsert(PlayViewModel vm)
        {
            Logger.Info("== Inserting data (Play) ==");
            if (Guid.Empty == vm.Id)
            {
                return  Add(vm);
            }
            else
            {
                return  Update(vm);
            }
        }

        public IEnumerable<GameDropDownViewModel> GetGameNameDrop()
        {
            return _PlayRepository.GetGameNameDrop();
        }

        public IEnumerable<SessionDeviceDropDownViewModel> GetUserSessionDeviceIdDrop()
        {
            return _PlayRepository.GetUserSessionDeviceIdDrop();
        }

        public IEnumerable<DeviceDropDownViewModel> GetUserDeviceCodeDrop()
        {
            return _PlayRepository.GetUserDeviceCodeDrop();
        }

        public IEnumerable<AccountDropDownViewModel> GetAccountNameDrop()
        {
            return _PlayRepository.GetAccountNameDrop();
        }

        public IEnumerable<GameRankingViewModel> GetRankingGames()
        {
            return _PlayRepository.CountPlaysPerGame();
        }
   
    }
}