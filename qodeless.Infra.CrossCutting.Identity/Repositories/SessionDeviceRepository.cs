using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class SessionDeviceRepository : Repository<SessionDevice>, ISessionDeviceRepository
    {
        public SessionDeviceRepository(ApplicationDbContext context) : base(context)
        {         
        }
        public IQueryable<Device> GetDevices()
        {
            return Db.Devices;
        }

        public IQueryable<DeviceDropDownViewModel> GetDevicesDrop()
        {
            return Db.Devices.Select(x => new DeviceDropDownViewModel { Id = x.Id, Code = x.Code }).AsQueryable();
        }

        public bool IsValidDeviceId(Guid deviceId)
        {
            return Db.Devices.Any(c => c.Id == deviceId);
        }

    }
}
