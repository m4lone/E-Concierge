using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface ISessionDeviceAppService : IDisposable
    {
        IEnumerable<SessionDeviceViewModel> GetAll();
        SessionDeviceViewModel GetById(Guid id);
        IEnumerable<SessionDeviceViewModel> GetAllBy(Func<SessionDevice, bool> exp);
        ValidationResult Add(SessionDeviceViewModel vm);
        ValidationResult Update(SessionDeviceViewModel vm);
        ValidationResult Upsert(SessionDeviceViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<DeviceViewModel> GetDevices();
        IEnumerable<DeviceDropDownViewModel> GetDevicesDrop();
        UserViewModel GetUserById(string userId);


    }
}