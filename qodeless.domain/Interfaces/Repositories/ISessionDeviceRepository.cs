using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ISessionDeviceRepository : IRepository<SessionDevice> //SOLID
    {
        IQueryable<Device> GetDevices();
        IQueryable<DeviceDropDownViewModel> GetDevicesDrop();
        bool IsValidDeviceId(Guid deviceId);
    }
}
