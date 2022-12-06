using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class SessionDeviceSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            const int TOTAL_ITEMS = 50;
            var Expire_DT = new DateTime(2021, 12, 31, 23, 59, 59);
            var users = _dbContext.Users.ToList();
            var devices = new DeviceRepository(_dbContext).GetAll().ToList();
            int count = 0;
            var sessionDevices = new List<SessionDevice>();

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionDevices.Add(new SessionDevice(Guid.NewGuid()) { DeviceId = GetFKRandom(devices), UserPlayId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, LastIpAddress = "192.168.2.2" });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionDevices.Add(new SessionDevice(Guid.NewGuid()) { DeviceId = GetFKRandom(devices), UserPlayId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, LastIpAddress = "192.168.2.12" });


            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionDevices.Add(new SessionDevice(Guid.NewGuid()) { DeviceId = GetFKRandom(devices), UserPlayId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, LastIpAddress = "192.168.2.21" });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionDevices.Add(new SessionDevice(Guid.NewGuid()) { DeviceId = GetFKRandom(devices), UserPlayId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, LastIpAddress = "192.168.2.1" });
            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionDevices.Add(new SessionDevice(Guid.NewGuid()) { DeviceId = GetFKRandom(devices), UserPlayId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, LastIpAddress = "192.168.2.51" });



            var sessionDeviceRepository = new SessionDeviceRepository(_dbContext);
            foreach (var sessiondevice in sessionDevices)
            {
                count++;
                sessionDeviceRepository.Upsert(sessiondevice, _ => _.DeviceId == sessiondevice.DeviceId, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER

        }
    }
}
