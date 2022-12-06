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
    public class AccountGameAppService : IAccountGameAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IAccountGameRepository _AccountGameRepository;
        private readonly IAccountRepository _AccountRepository;
        private readonly IGameRepository _GameRepository;
        public AccountGameAppService(IMapper mapper, IAccountGameRepository AccountGameRepository, IAccountRepository AccountRepository, IGameRepository GameRepository)
        {
            _mapper = mapper;
            _AccountGameRepository = AccountGameRepository;
            _AccountRepository = AccountRepository;
            _GameRepository = GameRepository;
        }

        public IEnumerable<AccountGameViewModel> GetAll()
        {
            Logger.Info("== Data displayed on the index ==");
            var accountGames = _AccountGameRepository.GetAll()
            .Include(_ => _.Account)
            .Include(_ => _.Game)
            .Select(_ => new AccountGameViewModel
            {
                Id = _.Id,
                GameId = _.Game.Id,
                GameName = _.Game.Name,
                AccountId = _.AccountId,
                AccountName = _.Account.Name,
             }).AsNoTracking();
                Dispose();
            return accountGames;
        }

        public AccountGameViewModel GetById(Guid id)
        {
            return _mapper.Map<AccountGameViewModel>(_AccountGameRepository.GetById(id));
        }

        public IEnumerable<AccountGameViewModel> GetAllBy(Func<AccountGame, bool> exp)
        {
            return _mapper.Map<IEnumerable<AccountGameViewModel>>(_AccountGameRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing data ==");
            var entity = _AccountGameRepository.GetById(id);
            var validationResult = new AccountGameRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _AccountGameRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(AccountGameViewModel vm)
        {
            Logger.Info("== Adding data ==");
            var entity = _mapper.Map<AccountGame>(vm);
            var validationResult = new AccountGameAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _AccountGameRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(AccountGameViewModel vm)
        {
            Logger.Info("== Updating data ==");
            var entity = _mapper.Map<AccountGame>(vm);
            var validationResult = new AccountGameUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _AccountGameRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(AccountGameViewModel vm)
        {
            Logger.Info("== Inserting data ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return  Update(vm);
        }

        public IEnumerable<AccountDropDownViewModel> GetAccountsDrop()
        {
            return _AccountGameRepository.GetAccountsDrop();
        }
        public IEnumerable<GameDropDownViewModel> GetGamesDrop()
        {
            return _AccountGameRepository.GetGamesDrop();
        }

        public IEnumerable<Game> GetGamesByAccountId(Guid accountId)
        {
            return _GameRepository.GetGamesByAccountId(accountId);
        }

        public ValidationResult UpdateGamesByAccountId(AccountGameMutiplevm accountGameMutiplevm)
        {
            Logger.Info("== Updating games by account ==");
            ValidationResult validationResult = null;
            var accountGames = GetAllBy(x => x.AccountId == accountGameMutiplevm.Account);
            if (accountGames != null)
            {
                foreach (var accountGame in accountGames)
                {
                    Remove(accountGame.Id);
                }
            }

            foreach (var gameId in accountGameMutiplevm.GameIds)
            {
                var entity = new AccountGame(accountGameMutiplevm.Account, gameId);
                validationResult = new AccountGameAddValidator().Validate(entity);
                if (validationResult.IsValid)
                    _AccountGameRepository.Add(entity);
            }
            return validationResult;
        }
    }
}
