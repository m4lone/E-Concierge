using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using qodeless.domain.Enums;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Device> GetDevices()
        {
            return Db.Devices.Where(_ => _.Excluded == false && _.DeletedAt == DateTime.UnixEpoch);
        }

        public IQueryable<Device> GetDevicesBySiteId(Guid siteId)
        {
            var deviceByUser = Db.SiteDevices.Where(x => x.SiteId == siteId && x.Excluded == false).Select(x => x.DeviceId).ToList();
            return Db.Devices.Where(x => deviceByUser.Any(a => a == x.Id));
        }



        public IQueryable<Device> GetDevicesBySiteDevice(Guid siteId)
        {
            var deviceByPartner = Db
                .SiteDevices
                .Where(x => x.SiteId == siteId && x.Excluded == false && x.DeletedAt == DateTime.UnixEpoch)
                .Select(x => x.DeviceId)
                .ToList();
            return Db.Devices.Where(x => deviceByPartner.Any(a => a == x.Id));
        }

        public IQueryable<DeviceGame> GetDevicesBySiteWithGames(IQueryable<Device> devices)
        {
            return (
                from devGame in Db.DeviceGames
                where devices.Any(x => x.Id.Equals(devGame.DeviceId))
                join game in Db.Games on devGame.GameId equals game.Id
                join device in Db.Devices on devGame.DeviceId equals device.Id
                select new DeviceGame(devGame.Id)
                {
                    Game = game,
                    Device = device,
                    GameId = game.Id,
                    DeviceId = device.Id
                }
            );
        }

        public IQueryable<DeviceStatusViewModel> CheckDeviceStatus()
        {
            var result = new List<DeviceStatusViewModel>();
            var devices = Db.Devices.ToList();
            foreach (var device in devices)
            {
                result.Add(new DeviceStatusViewModel { Device = device.Code, Status = device.Status.ToString() });
            }
            return result.OrderByDescending(_ => _.Status).AsQueryable();
        }
        public IQueryable<DeviceRankingViewModel> DevicesRanking()
        {
            var result = new List<DeviceRankingViewModel>();
            var devices = Db.Devices.ToList();

            foreach (var device in devices)
            {
                var plays = Db.Plays.ToList().Where(_ => _.DeviceId == device.Id).Count();
                result.Add(new DeviceRankingViewModel { Device = device.Code, Plays = plays });
            }
            return result.OrderByDescending(_ => _.Plays).AsQueryable();
        }
        public IQueryable<Device> GetActiveDevices()
        {
            var devices = Db.Devices.Where(_ => _.Status == EDeviceStatus.Actived).ToList().AsQueryable();
            return devices;
        }
    }
}
