using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class SiteDeviceSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            const int TOTAL_ITEMS = 200;
            int count = 0;
            #region DEVICESEEDER
            var devices = new DeviceRepository(_dbContext).GetAll().ToList();
            var sites = new SiteRepository(_dbContext).GetAll().ToList();

            var siteDevices = new List<SiteDevice>();
            for (int i = 0; i < TOTAL_ITEMS; i++)
                siteDevices.Add(new SiteDevice(Guid.NewGuid()) { SiteId = GetFKRandom(sites), DeviceId = GetFKRandom(devices) });
            
            var siteDeviceRepository = new SiteDeviceRepository(_dbContext);
            foreach (var siteDevice in siteDevices)
            {
                if (siteDeviceRepository.GetAllBy(_ => _.SiteId == siteDevice.SiteId).Count() >= 50) //Max 50 randomic items
                    continue;
                count++;
                siteDeviceRepository.AddNewer(siteDevice, _ => _.DeviceId == siteDevice.DeviceId && _.SiteId == siteDevice.SiteId, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //SITEDEVICESEEDER
        }       
    }
}
