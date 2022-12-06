using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface ISiteDeviceRepository : IRepository<SiteDevice> //SOLID
    {
        IQueryable<SiteDropDownViewModel> GetSitesDrop();
        IQueryable<Site> GetSites();
        IQueryable<Device> GetDevices();
        bool IsValidSiteId(Guid siteId);
        bool IsValidDeviceId(Guid deviceId);
        IQueryable<DeviceDropDownViewModel> GetDevicesDrop();
    }
}
