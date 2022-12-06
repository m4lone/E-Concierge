using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IDeviceRepository : IRepository<Device> //SOLID
    {
        IQueryable<Device> GetDevices();
        IQueryable<Device> GetDevicesBySiteId(Guid deviceId);
        IQueryable<Device> GetDevicesBySiteDevice(Guid siteid);
        IQueryable<DeviceGame> GetDevicesBySiteWithGames(IQueryable<Device> devices);
        public IQueryable<DeviceRankingViewModel> DevicesRanking();
        public IQueryable<Device> GetActiveDevices();
        public IQueryable<DeviceStatusViewModel> CheckDeviceStatus();
    }
}