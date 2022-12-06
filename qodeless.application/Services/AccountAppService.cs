using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Validators;
using qodeless.services.api.SysCobrancaClient.Managers.Zenvia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class AccountAppService : IAccountAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ISiteRepository _siteRepository;

        public AccountAppService(IMapper mapper, IAccountRepository accountRepository, ISiteRepository siteRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _siteRepository = siteRepository;
        }
        public ValidationResult UpdateBlockAccount(Guid id)
        {
            //Logger.Warn("Warning message of updating a blocked account");
            var entity = _accountRepository.GetById(id);
            var validationResult = new AccountUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
            {
                _accountRepository.UpdateBlockAccount(entity);

                if (validationResult.Errors.Count == 0)
                {
                    //SendSms or Whatsapp notification
                    // Logger.Error("====Error while trying to update====");
                }
            }

            return validationResult;
        }

        public ValidationResult UpdateUnlockAccount(Guid id)
        {
            //Logger.Info("Updating an unlocked account");
            var entity = _accountRepository.GetById(id);
            if (entity.Excluded == true || entity.DeletedAt != DateTime.UnixEpoch)
            {
                throw new Exception("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }

            var validationResult = new AccountUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
            {
                _accountRepository.UpdateUnlockAccount(entity);

                if (validationResult.Errors.Count == 0)
                {
                    //SendSms or Whatsapp notification
                    //Logger.Error("====Error while trying to update====");
                }
            }

            return validationResult;

        }
        public IEnumerable<AccountViewModel> GetAll()
        {
            var result = _mapper.Map<IEnumerable<AccountViewModel>>(_accountRepository.GetAll().Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch));
            return result;
        }

        public IEnumerable<SiteViewModel> GetSites(Guid accountId)
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_siteRepository.GetAllBy(c => c.AccountId == accountId).Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch));
        }

        public IEnumerable<AccountViewModel> GetAllIndex()
        {
            Logger.Info("== Accounts displayed on the index ==");
            return _accountRepository.GetAll().Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch).Select(x => new AccountViewModel
            {
                //campos que vão ser utilizados na INDEX
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Status = x.Status
            });
        }

        public AccountViewModel GetById(Guid id)
        {
            var entity = _accountRepository.GetById(id);
            if (entity.DeletedAt > DateTime.Now || entity.Excluded == true)
            {
                throw new InvalidOperationException("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }

            return _mapper.Map<AccountViewModel>(entity);
        }

        public IEnumerable<AccountViewModel> GetAllBy(Func<Account, bool> exp)
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(_accountRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing account ==");
            var entity = _accountRepository.GetById(id);
            var validationResult = new AccountRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _accountRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(AccountViewModel vm)
        {
            Logger.Info("== Adding new account ==");
            var entity = _mapper.Map<Account>(vm);
            var validationResult = new AccountAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _accountRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Upsert(AccountViewModel vm)
        {
            Logger.Info("== Inserting account ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }

        public ValidationResult Update(AccountViewModel vm)
        {
            Logger.Info("== Updating account ==");
            var exist = _accountRepository.GetAll().Where(_ => _.Id == vm.Id).FirstOrDefault();
            if (exist == null || exist.Excluded == true)
            {
                throw new Exception("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }
            var entity = _mapper.Map<Account>(vm);
            var validationResult = new AccountUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
            {
                _accountRepository.Update(entity);

                if ((EAccountStatus)Convert.ToInt32(vm.Status) == EAccountStatus.Actived)
                {
                    // Logger.Info("== Updating data according to the account's status ==");
                    var notifyMsg = $"*RG DIGITAL ALERTA!*\r\n\r\n\r\nCONTA: {vm.Name}\r\nData: {DateTime.Now.ToShortDateString()}\r\nHora: {DateTime.Now.ToShortTimeString()}\r\nStatus: *ATIVO*\r\n";
                    WhatsappSenderManager.SendWhatsapp("5511945464365", notifyMsg);
                }
                else
                {
                    // Logger.Info("== Updating data according to the account's status ==");
                    var notifyMsg = $"*RG DIGITAL ALERTA!*\r\n\r\n\r\nCONTA: {vm.Name}\r\nData: {DateTime.Now.ToShortDateString()}\r\nHora: {DateTime.Now.ToShortTimeString()}\r\nStatus: *BLOQUEADO*\r\n";
                    WhatsappSenderManager.SendWhatsapp("5511945464365", notifyMsg);
                }
            }

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public IEnumerable<AccountViewModel> GetAccounts()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(_accountRepository.GetAccounts());
        }
        public IEnumerable<UserPlaysRankingViewModel> GetUserPlaysRanking()
        {
            return _accountRepository.UserPlaysRanking();
        }

        public IEnumerable<PartnersRankingViewModel> GetPartnersRanking()
        {
            return _accountRepository.PartnersRanking();
        }

        public IEnumerable<ActivityStatusViewModel> GetOnlineUsers()
        {
            return _accountRepository.GetOnlineUsers();
        }

    }
}
