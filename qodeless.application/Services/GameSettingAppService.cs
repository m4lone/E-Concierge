using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NLog;
using qodeless.domain.Model;

namespace qodeless.application
{
    public class GameSettingAppService : IGameSettingAppService
    {

        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IGameSettingRepository _gameSettingRepository;
        public GameSettingAppService(IMapper mapper, IGameSettingRepository gameSettingRepository)
        {
            _mapper = mapper;
            _gameSettingRepository = gameSettingRepository;
        }

        public IEnumerable<GameSettingViewModel> GetAll()
        {

            var gameSettings = _mapper
                .Map<IEnumerable<GameSettingViewModel>>(_gameSettingRepository
                .GetAll()
                .Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch).AsNoTracking());
            Dispose();

            return gameSettings;
        }

        public GameSettingViewModel GetById(Guid id)
        {
            var gameSettings = _gameSettingRepository.GetById(id);
            if (gameSettings.Excluded == true || gameSettings.DeletedAt != DateTime.UnixEpoch)
            {
                throw new Exception("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }
            return _mapper.Map<GameSettingViewModel>(gameSettings);
        }
        public IEnumerable<GameSettingViewModel> GetAllBy(Func<GameSetting, bool> exp)
        {
            return _mapper.Map<IEnumerable<GameSettingViewModel>>(_gameSettingRepository.GetAllBy(exp).AsNoTracking());
        }

        public GameSettingViewModel GetBy(Func<GameSetting, bool> exp)
        {
            return _mapper.Map<GameSettingViewModel>(_gameSettingRepository.GetBy(exp));
        }
        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing gameSetting ==");
            var exist = GetById(id);
            var entity = _gameSettingRepository.GetById(id);
            var validationResult = new GameSettingRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _gameSettingRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(GameSettingViewModel vm)
        {
            Logger.Info("== Adding gameSetting ==");
            var entity = _mapper.Map<GameSetting>(vm);
            var validationResult = new GameSettingAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _gameSettingRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(GameSettingViewModel vm)
        {
            Logger.Info("== Updating gameSetting ==");
            var exist = GetById(vm.Id);
            var entity = _mapper.Map<GameSetting>(vm);
            var validationResult = new GameSettingUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _gameSettingRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(GameSettingViewModel vm)
        {
            Logger.Info("== Inserting gameSetting ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }
    }
}

