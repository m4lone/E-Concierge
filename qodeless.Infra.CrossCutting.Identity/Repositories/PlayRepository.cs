using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class PlayRepository : Repository<Play>, IPlayRepository
    {
        public PlayRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Device> GetDevices()
        {
            return Db.Devices;
        }

        public IQueryable<SessionDevice> GetSessionDevices()
        {
            return Db.SessionDevices;
        }
        public IQueryable<Game> GetGames()
        {
            return Db.Games;
        }

        public IQueryable<Account> GetAccounts()
        {
            return Db.Accounts;
        }

        public IQueryable<Play> GetPlayersBySiteId(Guid siteId)
        {
            var playsBySites = Db
                .SitePlayers
                .Where(x => x.Id == siteId)
                .Select(x => x.UserPlayerId)
                .ToList();
            return Db.Plays.Where(x => playsBySites.Any(a => a == x.UserPlayId));
        }

        public IQueryable<Play> GetPlaysBySiteId(Guid siteId)
        {
            return Db.Plays.Where(x => x.SiteId == siteId).AsQueryable();
        }

        public IQueryable<SessionDeviceDropDownViewModel> GetUserSessionDeviceIdDrop()
        {
            return Db.SessionDevices.Select(x => new SessionDeviceDropDownViewModel { Id = x.Id, UserPlayId = x.UserPlayId }).AsQueryable();
        }

        public IQueryable<DeviceDropDownViewModel> GetUserDeviceCodeDrop()
        {
            return Db.Devices.Select(x => new DeviceDropDownViewModel { Id = x.Id, Code = x.Code }).AsQueryable();
        }

        public IQueryable<GameDropDownViewModel> GetGameNameDrop()
        {
            return Db.Games.Select(x => new GameDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }

        public IQueryable<AccountDropDownViewModel> GetAccountNameDrop()
        {
            return Db.Accounts.Select(x => new AccountDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }

        public IQueryable<GameRankingViewModel> CountPlaysPerGame()
        {
            var result = new List<GameRankingViewModel>();
            var games = Db.Games.ToList();

            foreach(var game in games)
            {
                var plays = Db.Plays.ToList().Where(_ => _.GameId == game.Id).Count();                
                result.Add(new GameRankingViewModel { Game = game.Name, Plays = plays });
            }            

            return result.OrderByDescending(_=>_.Plays).AsQueryable();

        }
    }
}