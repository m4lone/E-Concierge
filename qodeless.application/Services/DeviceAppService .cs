using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class DeviceAppService : IDeviceAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceGameRepository _deviceGameRepository;
        public DeviceAppService(IMapper mapper, IDeviceRepository DeviceRepository, IDeviceGameRepository DeviceGameRepository)
        {
            _mapper = mapper;
            _deviceRepository = DeviceRepository;
            _deviceGameRepository = DeviceGameRepository;
        }

        public IEnumerable<DeviceViewModel> GetAll()
        {
            var devices = _deviceRepository.GetAll().Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch).AsNoTracking();
            var result = _mapper.Map<IEnumerable<DeviceViewModel>>(devices);
            Dispose();
            return result;
        }

        public DeviceViewModel GetById(Guid id)
        {
            var device = _deviceRepository.GetById(id);
            if (device.Excluded == true || device.DeletedAt != DateTime.UnixEpoch)
            {
                throw new Exception("ID DOES NOT EXIST OR HAS BEEN DELETED");
            }
            return _mapper.Map<DeviceViewModel>(device);
        }

        public IEnumerable<DeviceViewModel> GetAllBy(Func<Device, bool> exp)
        {
            return _mapper.Map<IEnumerable<DeviceViewModel>>(_deviceRepository.GetAllBy(exp).AsNoTracking());
        }


        public IEnumerable<DeviceViewModel> GetAllIndex()
        {
            Logger.Info("== Devices that are displayed on the index ==");
            var devices = _deviceRepository.GetAll().Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch);
            var index = devices.Select(x => new DeviceViewModel
            {
                //campos que vão ser utilizados na INDEX
                SerialNumber = x.SerialNumber,
                Code = x.Code,
                Type = x.Type,
                Id = x.Id,
                Availability = x.Availability,
                MacAddress = x.MacAddress,
                Status = x.Status
            });
            return index;
        }
        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing device ==");
            var exist = GetById(id);
            var entity = _deviceRepository.GetById(id);
            var validationResult = new DeviceRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _deviceRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(DeviceViewModel vm)
        {
            Logger.Info("== Adding device ==");
            var entity = _mapper.Map<Device>(vm);
            var validationResult = new DeviceAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _deviceRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(DeviceViewModel vm)
        {
            Logger.Info("== Updating device ==");
            var exist = GetById(vm.Id);
            var entity = _mapper.Map<Device>(vm);
            var validationResult = new DeviceUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _deviceRepository.Update(entity);

            return validationResult;
        }
        public ValidationResult Upsert(DeviceViewModel vm)
        {
            Logger.Info("== Inserting device ==");
            if (Guid.Empty == vm.Id)
                return Add(vm);
            else
                return Update(vm);
        }
        public void Dispose() => GC.SuppressFinalize(this);

        public IEnumerable<DeviceViewModel> GetDevices()
        {
            var devices = _deviceRepository.GetDevices();
            return _mapper.Map<IEnumerable<DeviceViewModel>>(devices);
        }

        public IEnumerable<DeviceViewModel> GetDevicesBySiteId(Guid deviceId)
        {
            Logger.Info("== Getting devices by site ==");
            return _mapper.Map<IEnumerable<DeviceViewModel>>(_deviceRepository.GetDevicesBySiteId(deviceId));
        }
        public IEnumerable<DeviceGame> GetDevicesBySiteIdWithGames(Guid siteId)
        {
            Logger.Info("== Getting devices by site ==");
            return _deviceRepository.GetDevicesBySiteWithGames(_deviceRepository.GetDevicesBySiteId(siteId));
        }
        public int GetTotalDevices()
        {
            Logger.Info("== Getting the total of devices ==");
            return _deviceRepository.GetAll().Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch).Count();
        }

        public IEnumerable<DeviceRankingViewModel> GetDevicesRanking()
        {
            return _deviceRepository.DevicesRanking();
        }

        public IEnumerable<DeviceViewModel> CheckActiveDevices()
        {
            Logger.Info("== Getting Active devices ==");
            var devices = _mapper.Map<IEnumerable<DeviceViewModel>>(_deviceRepository.GetActiveDevices());
            return devices;
        }
        public IEnumerable<DeviceStatusViewModel> GetDeviceStatus()
        {
            Logger.Info("== Getting devices status ==");
            return _deviceRepository.CheckDeviceStatus();
        }
    }
}
