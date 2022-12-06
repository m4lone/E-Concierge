using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application.Interfaces
{
    public interface IDeviceAppService : IDisposable
    {
        IEnumerable<DeviceViewModel> GetAll();
        DeviceViewModel GetById(Guid id);
        IEnumerable<DeviceViewModel> GetAllBy(Func<Device, bool> exp);
        IEnumerable<DeviceViewModel> GetAllIndex();
        ValidationResult Add(DeviceViewModel vm);
        ValidationResult Update(DeviceViewModel vm);
        ValidationResult Remove(Guid id);
        ValidationResult Upsert(DeviceViewModel vm);
        IEnumerable<DeviceViewModel> GetDevices();
        IEnumerable<DeviceViewModel> GetDevicesBySiteId(Guid id);
        public int GetTotalDevices();
        IEnumerable<DeviceGame> GetDevicesBySiteIdWithGames(Guid siteId);
        public IEnumerable<DeviceStatusViewModel> GetDeviceStatus();
        public IEnumerable<DeviceRankingViewModel> GetDevicesRanking();
        public IEnumerable<DeviceViewModel> CheckActiveDevices();

    }
}
