using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class SiteDeviceRepository : Repository<SiteDevice>, ISiteDeviceRepository
    {
        public SiteDeviceRepository(ApplicationDbContext context) : base(context)
        {
        }
        public bool IsValidSiteId(Guid siteId)
        {
            return Db.Sites.Any(c => c.Id == siteId);
        }
        public IQueryable<Site> GetSites()
        {
            return Db.Sites;
        }
        public IQueryable<SiteDropDownViewModel> GetSitesDrop()
        {
            return Db.Sites.Select(x => new SiteDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }
        public bool IsValidDeviceId(Guid deviceId)
        {
            return Db.Devices.Any(c => c.Id == deviceId);
        }

        public IQueryable<Device> GetDevices()
        {
            return Db.Devices;
        }

        public IQueryable<DeviceDropDownViewModel> GetDevicesDrop()
        {
            return Db.Devices.Select(x => new DeviceDropDownViewModel { Id = x.Id,Code = x.Code }).AsQueryable();
        }
    }
}
