using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application.Interfaces
{
    public interface IDeviceGameAppService : IDisposable
    {
        IEnumerable<DeviceGameViewModel> GetAll();
        DeviceGameViewModel GetById(Guid id);
        IEnumerable<DeviceGameViewModel> GetAllBy(Func<DeviceGame, bool> exp);
        ValidationResult Add(DeviceGameViewModel vm);
        ValidationResult Update(DeviceGameViewModel vm);
        ValidationResult Remove(Guid id);
        ValidationResult Upsert(DeviceGameViewModel vm);
        IEnumerable<DeviceDropDownViewModel> GetDevicesDrop();
        IEnumerable<GameDropDownViewModel> GetGamesDrop();
        DeviceGameViewModel GetGameByDeviceId(Guid id);
    }
}
