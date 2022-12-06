using AutoMapper;
using FluentValidation.Results;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using AutoMapper;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static qodeless.domain.Validators.SiteDeviceUpdateValidator;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using qodeless.domain.Model;
using NLog;
using qodeless.domain.Enums;

namespace qodeless.application.Services
{

    public class SiteDeviceAppService : ISiteDeviceAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ISiteDeviceRepository _siteDeviceRepository;
        private readonly IDeviceRepository _deviceRepository;
        public SiteDeviceAppService(IMapper mapper, ISiteDeviceRepository SiteDeviceRepository, IDeviceRepository deviceRepository)
        {
            _mapper = mapper;
            _siteDeviceRepository = SiteDeviceRepository;
            _deviceRepository = deviceRepository;
        }

        public IEnumerable<SiteDeviceViewModel> GetAll()
        {
            Logger.Info("== SiteDevice displayed on the index ==");
            var siteDevices = _siteDeviceRepository.GetAll()
                .Include(_ => _.Site).ThenInclude(c=>c.Account)
                .Include(_ => _.Device)
                .Select(_ => new SiteDeviceViewModel
                {
                    Id = _.Id,
                    SiteId = _.SiteId,
                    SiteName = _.Site.Name,     
                    DeviceId = _.Device.Id,
                    DeviceName = _.Device.Code,
                }).AsNoTracking();
            Dispose();
            return siteDevices;
        }
        public IEnumerable<Device> GetAllFreeDevice()
        {
            Logger.Info("== SiteDevice displayed on the index ==");
            var siteDevices = _siteDeviceRepository.GetAll().Select(_ => _.DeviceId).ToList();
            var result = _deviceRepository.GetAllBy(x => !siteDevices.Any(i => i == x.Id) && x.Status == EDeviceStatus.Actived);

            Dispose();
            return result;
        }
        public IEnumerable<Device> GetDevicesBySiteDevice(Guid siteId)
        {
            return _deviceRepository.GetDevicesBySiteDevice(siteId);
        }

        public SiteDeviceViewModel GetById(Guid id)
        {
            return _mapper.Map<SiteDeviceViewModel>(_siteDeviceRepository.GetById(id));
        }
        public IEnumerable<SiteViewModel> GetSites()
        {
            return _mapper.Map<IEnumerable<SiteViewModel>>(_siteDeviceRepository.GetSites());
        }
        public IEnumerable<DeviceViewModel> GetDevices()
        {
            return _mapper.Map<IEnumerable<DeviceViewModel>>(_siteDeviceRepository.GetDevices());
        }
        public IEnumerable<SiteDeviceViewModel> GetAllBy(Func<SiteDevice, bool> exp)
        {
            return _mapper.Map<IEnumerable<SiteDeviceViewModel>>(_siteDeviceRepository.GetAllBy(exp));
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing site device ==");
            var entity = _siteDeviceRepository.GetById(id);
            var validationResult = new SiteDeviceRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _siteDeviceRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(SiteDeviceViewModel vm)
        {
            Logger.Info("== Adding site device ==");
            var entity = _mapper.Map<SiteDevice>(vm);
            var validationResult = new SiteDeviceAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _siteDeviceRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(SiteDeviceViewModel vm)
        {
            Logger.Info("== Updating site device ==");
            var entity = _mapper.Map<SiteDevice>(vm);
            var validationResult = new SiteDeviceUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _siteDeviceRepository.Update(entity);

            return validationResult;
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public ValidationResult Upsert(SiteDeviceViewModel vm)
        {
            Logger.Info("== Inserting site device ==");
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
            return _siteDeviceRepository.GetSitesDrop();
        }


        public IEnumerable<DeviceDropDownViewModel> GetDevicesDrop()
        {
            return _siteDeviceRepository.GetDevicesDrop();
        }


        public SiteDeviceMultipleVm GetDevicesBySiteId(Guid site)
        {
            var vm = new SiteDeviceMultipleVm()
            {
                Site = site,
                DeviceIds = _siteDeviceRepository.GetAllBy(c => c.Id == site).Select(c => c.DeviceId).ToList()
            };
            return vm;
        }


        public ValidationResult UpdateDeviceBySiteId(SiteDeviceMultipleVm SiteDeviceMultipleVm)
        {
            Logger.Info("== Updating games by account ==");
            ValidationResult validationResult = null;
            var siteDevices = GetAllBy(x => x.SiteId == SiteDeviceMultipleVm.Site);
            if (siteDevices != null)
            {
                foreach (var siteDevice in siteDevices)
                {
                    Remove(siteDevice.Id);
                }
            }

            foreach (var deviceId in SiteDeviceMultipleVm.DeviceIds)
            {
                var entity = new SiteDevice(SiteDeviceMultipleVm.Site, deviceId);
                validationResult = new SiteDeviceAddValidator().Validate(entity);
                if (validationResult.IsValid)
                    _siteDeviceRepository.Add(entity);
            }
            return validationResult;
        }
        public IEnumerable<SiteDeviceViewModel> GetSiteDevice(Guid siteId)
        {
            return _mapper.Map<IEnumerable<SiteDeviceViewModel>>(_siteDeviceRepository.GetAllBy(c => c.SiteId == siteId));
        }

    }
}
