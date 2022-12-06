using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Validators;
using qodeless.Infra.CrossCutting.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class SiteAppService : ISiteAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ISiteRepository _siteRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public SiteAppService(IMapper mapper, ISiteRepository SiteRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _siteRepository = SiteRepository;
            _userManager = userManager;
    }

        public IEnumerable<SiteOperatorViewModel> GetOperators(Guid? employerId)
        {
            var operators = _siteRepository.GetOperators(employerId);
            return operators;
        }

        public IEnumerable<SiteViewModel> GetAll()
        {

            var site = _siteRepository.GetAll()
               .Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch)
               .Include(_ => _.Account)
               .Select(_ => new SiteViewModel
               {
                  Id = _.Id,
                   Name = _.Name,
                   Description = _.Description,
                   ZipCode = _.ZipCode,
                   Address = _.Address,
                   Number = _.Number,
                   City = _.City,
                   State = _.State,
                   Country = _.Country,
                   AccountId = _.AccountId,
                   ESiteType = _.ESiteType,
                   Code = _.Code,
                   Email = _.Email,
                   CellPhone = _.CellPhone
               }).AsNoTracking();
            Dispose();
            return site;
        }
        public IEnumerable<SiteViewModel> GetAllIndex()
        {
            Logger.Info("== Site displayed on the index ==");
            return _siteRepository.GetAll()
                .Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch)
                .Select(_ => new SiteViewModel
            {
                //campos que vão ser utilizados na INDEX
                Id = _.Id,
                Name = _.Name,
                Description = _.Description,
                ZipCode = _.ZipCode,
                Address = _.Address,
                Number = _.Number,
                City = _.City,
                State = _.State,
                Country = _.Country,
                AccountId = _.AccountId,
                ESiteType = _.ESiteType,
                Code = _.Code,

                //continuar expressão até abranger todos os resultados possiveis do enumerador
                //TextType = _.ESiteType == ESiteType.Casino ? "Cassino" : (_.ESiteType == ESiteType.Default ? "Padrão" : "Inativo")
            });
        }

        public IEnumerable<AccountDropDownViewModel> GetAccountsDropDown()
        {
            return _siteRepository.GetAccountsDropDown();
        }

        public SiteViewModel GetById(Guid id)
        {
            var entity = _siteRepository.GetById(id);
            if (entity.DeletedAt != DateTime.UnixEpoch || entity.Excluded == true)
            {
                throw new Exception("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }

            return _mapper.Map<SiteViewModel>(entity);
        }

        public SiteViewModel GetBy(Func<Site, bool> exp)
        {
            var site = _siteRepository.GetBy(exp);
            return _mapper.Map<SiteViewModel>(site);
        }

        //public SiteViewModel> GetByCode(string code)
        //{
        //    return _mapper.Map<SiteViewModel>(_siteRepository.GetByCode(code));
        //}

        public IEnumerable<SiteViewModel> GetAllBy(Func<Site, bool> exp)
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_siteRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing site ==");
            var exist = GetById(id);
            var entity = _siteRepository.GetById(id);
            var validationResult = new SiteRemoveValidator(_siteRepository).Validate(entity);
            if (validationResult.IsValid)
                _siteRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(SiteViewModel vm)
        {
            Logger.Info("== Adding site ==");
            var entity = _mapper.Map<Site>(vm);
            var validationResult = new SiteAddValidator(_siteRepository).Validate(entity);
            if (validationResult.IsValid)
                _siteRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(SiteViewModel vm)
        {
            Logger.Info("== Updating site ==");
            var entity = _mapper.Map<Site>(vm);
            var validationResult = new SiteUpdateValidator(_siteRepository).Validate(entity);
            if (validationResult.IsValid)
                _siteRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public IEnumerable<AccountViewModel> GetAccounts()
        {
            return _mapper.Map<IEnumerable<AccountViewModel>>(_siteRepository.GetAccounts());
        }
        public ValidationResult Upsert(SiteViewModel vm)
        {
            Logger.Info("== Inserting site ==");
            if (Guid.Empty == vm.Id)
            {
                return Add(vm);
            }
            else
            {
                return Update(vm);
            }
        }
        public SiteViewModel GetTotalSites()
        {
            Logger.Info("== Getting the total of sites ==");
            var totalSites = _siteRepository.GetAll().Where(_=>_.Excluded == false).Count();
            return new SiteViewModel { TotalSites = totalSites };

        }
        public List<int> GetCurrencyGame(Guid siteId)
        {
            return _siteRepository.GetById(siteId).GameCurrencies;
        }
        public ValidationResult UpdateCurrencyGameBySiteId(CurrencyGameViewModel currencyGame)
        {
            var site = _siteRepository.GetById(currencyGame.SiteId);
            site.GameCurrencies = new List<int>();
            foreach (var gameCurrency in currencyGame.GameCurrencies)
            {
                site.GameCurrencies.Add((int)gameCurrency);
            }
            var validationResult = new SiteUpdateValidator(_siteRepository).Validate(site);
            if (validationResult.IsValid)
                _siteRepository.Update(site);

            return validationResult;
        }

        public IEnumerable<SiteRankingViewModel> GetSitesRanking()
        {
            return _siteRepository.SitesRanking();
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString().ToUpper();
        }
    }
}
