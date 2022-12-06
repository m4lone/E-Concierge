using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.application.Interfaces;
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
    public class DeviceGameAppService : IDeviceGameAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IDeviceGameRepository _deviceGameRepository;
        public DeviceGameAppService(IMapper mapper, IDeviceGameRepository DeviceGameRepository)
        {
            _mapper = mapper;
            _deviceGameRepository = DeviceGameRepository;
        }

        public IEnumerable<DeviceGameViewModel> GetAll()
        {
            Logger.Info("== Data(DeviceGame) that's displayed on the index ==");
            var deviceGames = _deviceGameRepository.GetAll()
            .Include(_ => _.Device)
            .Include(_ => _.Game)
            .Select(_ => new DeviceGameViewModel
            {
                Id = _.Id,
                GameId = _.Game.Id,
                GameName = _.Game.Name,
                DeviceId = _.DeviceId,
                DeviceName = _.Device.Code,
            }).AsNoTracking();
            Dispose();
            return deviceGames;
        }
        public DeviceGameViewModel GetGameByDeviceId(Guid id)
        {
            Logger.Info("== Data(DeviceGame) that's displayed on the index ==");
            var deviceGame = _deviceGameRepository.GetAll()
            .Include(_ => _.Device)
            .Include(_ => _.Game)
            .Where(x => x.DeviceId.Equals(id))
            .Select(_ => new DeviceGameViewModel
            {
                Id = _.Id,
                GameId = _.Game.Id,
                GameName = _.Game.Name,
                DeviceId = _.DeviceId,
                DeviceName = _.Device.Code,
            }).FirstOrDefault();
            Dispose();
            return deviceGame;
        }

        public DeviceGameViewModel GetById(Guid id)
        {
            return _mapper.Map<DeviceGameViewModel>(_deviceGameRepository.GetById(id));
        }

        public IEnumerable<DeviceGameViewModel> GetAllBy(Func<DeviceGame, bool> exp)
        {
            return _mapper.Map<IEnumerable<DeviceGameViewModel>>(_deviceGameRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing data (DeviceGame) ==");
            var entity = _deviceGameRepository.GetById(id);
            var validationResult = new DeviceGameRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _deviceGameRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(DeviceGameViewModel vm)
        {
            Logger.Info("== Adding data (DeviceGame) ==");
            var entity = _mapper.Map<DeviceGame>(vm);
            var validationResult = new DeviceGameAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _deviceGameRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(DeviceGameViewModel vm)
        {
            Logger.Info("== Updating data (DeviceGame) ==");
            var entity = _mapper.Map<DeviceGame>(vm);
            var validationResult = new DeviceGameUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _deviceGameRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(DeviceGameViewModel vm)
        {
            Logger.Info("== Inserting data (DeviceGame) ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }

        public IEnumerable<DeviceDropDownViewModel> GetDevicesDrop()
        {
            return _deviceGameRepository.GetDevicesDrop();
        }

        public IEnumerable<GameDropDownViewModel> GetGamesDrop()
        {
            return _deviceGameRepository.GetGamesDrop();
        }
    }
}
