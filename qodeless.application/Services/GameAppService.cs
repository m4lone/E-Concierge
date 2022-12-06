using AutoMapper;
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
    public class GameAppService : IGameAppService
    {

        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;
        public GameAppService(IMapper mapper, IGameRepository GameRepository)
        {
            _mapper = mapper;
            _gameRepository = GameRepository;
        }

        public IEnumerable<GameViewModel> GetAll()
        {

            var games = _mapper
                .Map<IEnumerable<GameViewModel>>(_gameRepository
                .GetAll()
                .Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch).AsNoTracking());
            Dispose();

            return games;
        }

        public GameViewModel GetById(Guid id)
        {
            var games = _gameRepository.GetById(id);
            if (games.Excluded == true || games.DeletedAt != DateTime.UnixEpoch)
            {
                throw new Exception("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }
            return _mapper.Map<GameViewModel>(games);
        }
        public IEnumerable<GameViewModel> GetGamesByAccountId(Guid accountId)
        {
            return _mapper.Map<IEnumerable<GameViewModel>>(_gameRepository.GetGamesByAccountId(accountId));
        }
        public IEnumerable<GameViewModel> GetAllBy(Func<Game, bool> exp)
        {
            return _mapper.Map<IEnumerable<GameViewModel>>(_gameRepository.GetAllBy(exp).AsNoTracking());
        }

        public IEnumerable<GameViewModel> GetAllIndex()
        {
            Logger.Info("== Games displayed on the index ==");
            return _gameRepository.GetAll().Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch).Select(x => new GameViewModel
            {
                //campos que vão ser utilizados na INDEX
                Name = x.Name,
                Code = x.Code,
                DtPublish = x.DtPublish,
                Id = x.Id,
                Version = x.Version,

                //continuar expressão até abranger todos os resultados possiveis do enumerador
                TextoStatus = x.Status == EGameStatus.Actived ? "Ativo" : (x.Status == EGameStatus.Blocked ? "Bloqueado" : "Inativo")
            });
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing game ==");
            var exist = GetById(id);
            var entity = _gameRepository.GetById(id);
            var validationResult = new GameRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _gameRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(GameViewModel vm)
        {
            Logger.Info("== Adding game ==");
            var entity = _mapper.Map<Game>(vm);
            var validationResult = new GameAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _gameRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(GameViewModel vm)
        {
            Logger.Info("== Updating game ==");
            var exist = GetById(vm.Id);
            var entity = _mapper.Map<Game>(vm);
            var validationResult = new GameUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _gameRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(GameViewModel vm)
        {
            Logger.Info("== Inserting game ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }
        public IEnumerable<GameViewModel> GetGames()
        {
            var games = _gameRepository.GetGames().Where(_=> _.DeletedAt == DateTime.UnixEpoch && _.Excluded == false);
            if (games.Count() == 0)
            {
                throw new Exception("NOT FOUND GAMES");
            }
            return _mapper.Map<IEnumerable<GameViewModel>>(games);
        }
        public IEnumerable<ProfitRankingViewModel> GetRankingGames()
        {
            return _gameRepository.ProfitRanking();
        }
    }
}

