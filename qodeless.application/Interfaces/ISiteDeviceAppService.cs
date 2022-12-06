using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application.Interfaces
{
    public interface ISiteDeviceAppService : IDisposable
    {
        IEnumerable<SiteDeviceViewModel> GetAll();
        SiteDeviceViewModel GetById(Guid id);
        IEnumerable<SiteDeviceViewModel> GetAllBy(Func<SiteDevice, bool> exp);
        ValidationResult Add(SiteDeviceViewModel vm);
        ValidationResult Update(SiteDeviceViewModel vm);
        ValidationResult Upsert(SiteDeviceViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<SiteViewModel> GetSites();
        IEnumerable<SiteDropDownViewModel> GetSitesDrop();
        IEnumerable<DeviceViewModel> GetDevices();
        IEnumerable<DeviceDropDownViewModel> GetDevicesDrop();
        IEnumerable<SiteDeviceViewModel> GetSiteDevice(Guid siteId);

        //Pesquisa Device by Site Id;
        SiteDeviceMultipleVm GetDevicesBySiteId(Guid accountId);
        IEnumerable<Device> GetDevicesBySiteDevice(Guid siteid);
        ValidationResult UpdateDeviceBySiteId(SiteDeviceMultipleVm siteDeviceMutiplevm);
        IEnumerable<Device> GetAllFreeDevice();
    }
}
