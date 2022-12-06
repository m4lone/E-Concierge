using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IPlayRepository : IRepository<Play> //SOLID
    {
        IQueryable<SessionDeviceDropDownViewModel> GetUserSessionDeviceIdDrop();
        IQueryable<DeviceDropDownViewModel> GetUserDeviceCodeDrop();
        IQueryable<GameDropDownViewModel> GetGameNameDrop();
        IQueryable<AccountDropDownViewModel> GetAccountNameDrop();
        IQueryable<Play> GetPlayersBySiteId(Guid sited);
        IQueryable<Device> GetDevices();
        IQueryable<SessionDevice> GetSessionDevices();
        IQueryable<Game> GetGames();
        IQueryable<Account> GetAccounts();
        IQueryable<Play> GetPlaysBySiteId(Guid siteId);
        IQueryable<GameRankingViewModel> CountPlaysPerGame();
    }
}