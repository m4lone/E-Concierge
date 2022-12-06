using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class VouscherAppService : IVouscherAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IVirtualBalanceRepository _virtualBalanceRepository;
        private readonly IVouscherRepository _vouscherRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IIncomeRepository _incomeRepository;

        public VouscherAppService(IMapper mapper, IVirtualBalanceRepository virtualBalanceRepository, IVouscherRepository vouscherRepository, ISiteRepository siteRepository, IIncomeRepository incomeRepository)
        {
            _mapper = mapper;
            _virtualBalanceRepository = virtualBalanceRepository;
            _vouscherRepository = vouscherRepository;
            _siteRepository = siteRepository;
            _incomeRepository = incomeRepository;
        }

        public IEnumerable<VouscherViewModel> GetAll()
        {
            Logger.Info("== Vouchers displayed on the index ==");
            return _mapper.Map<IEnumerable<VouscherViewModel>>(_vouscherRepository.GetAll());
        }

        public VouscherViewModel GetById(Guid id)
        {
            var response = _mapper.Map<VouscherViewModel>(_vouscherRepository.GetById(id));
            Dispose();
            return response;
        }

        public IEnumerable<VouscherViewModel> GetAllBy(Func<Voucher, bool> exp)
        {
            return _mapper.Map<IEnumerable<VouscherViewModel>>(_vouscherRepository.GetAllBy(exp));
        }
        public IEnumerable<VouscherViewModel> GetAllBySiteId(Func<Voucher, bool> exp)
        {
            var response = _vouscherRepository.GetAll()
                .Include(x => x.Site)
                .Where(exp)
                .Select(x => new VouscherViewModel()
                {
                    SiteName = x.Site.Name,
                    Amount = x.Amount,
                    DueDate = x.DueDate,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    SiteID = x.SiteID,
                    QrCodeKey = x.QrCodeKey,
                    UserOperationId = x.UserOperationId
                });
            Dispose();
            return response;
        }
        public VouscherViewModel GetBy(Func<Voucher, bool> exp)
        {
            return _mapper.Map<VouscherViewModel>(_vouscherRepository.GetBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing voucher ==");
            var entity = _vouscherRepository.GetById(id);
            var validationResult = new VouscherRemoveValidator(_vouscherRepository).Validate(entity);
            if (validationResult.IsValid)
                _vouscherRepository.SoftDelete(entity);

            return validationResult;
        }

        public ValidationResult Add(VouscherViewModel vm)
        {
            Logger.Info("== Adding voucher ==");
            var entity = _mapper.Map<Voucher>(vm);
            var validationResult = new VouscherAddValidator(_vouscherRepository).Validate(entity);
            if (validationResult.IsValid)
                _vouscherRepository.Add(entity);

            return validationResult;
        }

        public ValidationResult Update(VouscherViewModel vm)
        {
            Logger.Info("== Updating voucher ==");
            var entity = _mapper.Map<Voucher>(vm);
            var validationResult = new VouscherAddValidator(_vouscherRepository).Validate(entity);
            if (validationResult.IsValid)
                _vouscherRepository.Update(entity);

            return validationResult;
        }
        public ValidationResult DisableVoucher(Guid id)
        {
            var voucher = _vouscherRepository.GetById(id);
            voucher.IsActive = false;

            var validationResult = new VouscherAddValidator(_vouscherRepository).Validate(voucher);
            if (validationResult.IsValid)
                _vouscherRepository.Update(voucher);

            return validationResult;
        }
        public string VoucherCode()
        {
            var number = "";
            var random = new Random();

            string letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder resultStringBuilder = new StringBuilder();

            for (int i = 0; i < 2; i++)
            {
                resultStringBuilder.Append(letter[random.Next(letter.Length)]);
            }

            for (int i = 0; i < 4; i++)
            {
                number += new Random().Next(0, 9).ToString();
            }

            string voucherCode = "RG" + number + resultStringBuilder;

            return voucherCode;
        }

        public string GenerateVoucherCode(VouscherViewModel vm)
        {
            var entity = _mapper.Map<Voucher>(vm);
            entity.QrCodeKey = VoucherCode();
            entity.DueDate = DateTime.Now.AddDays(2);
            var site = _siteRepository.GetById(vm.SiteID);
            var validationResult = new VouscherAddValidator(_vouscherRepository).Validate(entity);
            var income = new Income(EIncomeType.Voucher, entity.Amount, "Venda de crédito via voucher", site.AccountId, site.Id);
            var virtualBalance = new VirtualBalance(entity.UserOperationId, EBalanceType.Credit, entity.Amount, "Venda de crédito via voucher", entity.Id);

            if (validationResult.IsValid)
            {
                _vouscherRepository.Add(entity);
                _incomeRepository.Add(income);
                _virtualBalanceRepository.Add(virtualBalance);
                return entity.QrCodeKey;
            }
            return string.Empty;
        }

        public object VerifyPayVoucherCode(List<string> voucherCode, string userPlayId, EBalanceType type, string description, string userOperationId)
        {
            var voucherList = _vouscherRepository.GetAll().Where(f => voucherCode.Contains(f.QrCodeKey)).ToList();
            var obj = new VouscherValidationViewModel() { };

            foreach (var voucher in voucherList)
            {
                var virtualBalance = new VirtualBalance(Guid.NewGuid())
                {
                    VoucherId = voucher.Id,
                    UserOperationId = userOperationId,
                    Amount = voucher.Amount,
                    UserPlayId = userPlayId,
                    Type = type,
                    Description = description,
                };

                if (DateTime.Now <= voucher.DueDate && !_virtualBalanceRepository.Any(_ => _.VoucherId == voucher.Id))
                {
                    obj.QrCodeKey = voucher.QrCodeKey;
                    obj.Expired = DateTime.Now <= voucher.DueDate;

                    _virtualBalanceRepository.Add(virtualBalance);
                }
                else
                {
                    obj.QrCodeKey = voucher.QrCodeKey;
                    obj.Expired = DateTime.Now >= voucher.DueDate;
                }
            }

            return obj;
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
