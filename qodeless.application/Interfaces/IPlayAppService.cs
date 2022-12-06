using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IPlayAppService : IDisposable
    {
        IEnumerable<PlayViewModel> GetAll();
        PlayViewModel GetById(Guid id);
        IEnumerable<PlayViewModel> GetAllBy(Func<Play, bool> exp);
        ValidationResult Add(PlayViewModel vm);
        ValidationResult Update(PlayViewModel vm);
        ValidationResult Remove(Guid id);
        IEnumerable<DeviceViewModel> GetDevices();
        IEnumerable<SessionDeviceViewModel> GetSessionDevices();
        IEnumerable<GameViewModel> GetGames();
        IEnumerable<AccountViewModel> GetAccounts();
        ValidationResult Upsert(PlayViewModel vm);
        IEnumerable<GameDropDownViewModel> GetGameNameDrop();
        IEnumerable<SessionDeviceDropDownViewModel> GetUserSessionDeviceIdDrop();
        IEnumerable<DeviceDropDownViewModel> GetUserDeviceCodeDrop();
        IEnumerable<AccountDropDownViewModel> GetAccountNameDrop();
        IEnumerable<PlayViewModel> GetBySiteId(Guid id,string userId);
        IEnumerable<GameRankingViewModel> GetRankingGames();

    }
}