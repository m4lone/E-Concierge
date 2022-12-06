using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;

namespace qodeless.DataManager.Seeds
{
    public class DeviceSeeder
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            #region DEVICESEEDER
            int count = 0;
            //PARA GERAR UM NOVO GUID: ACESSE ==> https://www.guidgenerator.com/online-guid-generator.aspx
            var deviceSeeds = new List<Device>() {
                new Device(Guid.NewGuid()){Code = "C121", SerialNumber = "SN721", Status = EDeviceStatus.Actived, MacAddress ="00:E0:4C:7D:9C:B2", Type = EDeviceType.Cabinet},
                new Device(Guid.NewGuid()){Code = "C122", SerialNumber = "SN722", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C123", SerialNumber = "SN723", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C124", SerialNumber = "SN724", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
                new Device(Guid.NewGuid()){Code = "C125", SerialNumber = "SN725", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C126", SerialNumber = "SN726", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C127", SerialNumber = "SN727", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C128", SerialNumber = "SN728", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C221", SerialNumber = "SN821", Status = EDeviceStatus.Actived, MacAddress ="00:E0:4C:7D:9C:B2", Type = EDeviceType.Cabinet},
                new Device(Guid.NewGuid()){Code = "C222", SerialNumber = "SN822", Status = EDeviceStatus.Blocked, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C223", SerialNumber = "SN823", Status = EDeviceStatus.Inatived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
                new Device(Guid.NewGuid()){Code = "C224", SerialNumber = "SN824", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C225", SerialNumber = "SN825", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C226", SerialNumber = "SN826", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
                new Device(Guid.NewGuid()){Code = "C227", SerialNumber = "SN827", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C228", SerialNumber = "SN828", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C231", SerialNumber = "SN921", Status = EDeviceStatus.Actived, MacAddress ="00:E0:4C:7D:9C:B2", Type = EDeviceType.LanPC},
                new Device(Guid.NewGuid()){Code = "C232", SerialNumber = "SN922", Status = EDeviceStatus.Blocked, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C233", SerialNumber = "SN923", Status = EDeviceStatus.Inatived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C234", SerialNumber = "SN924", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C235", SerialNumber = "SN925", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C236", SerialNumber = "SN926", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C237", SerialNumber = "SN927", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C238", SerialNumber = "SN928", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
                new Device(Guid.NewGuid()){Code = "C221", SerialNumber = "SS721", Status = EDeviceStatus.Actived, MacAddress ="00:E0:4C:7D:9C:B2", Type = EDeviceType.Cabinet},
                new Device(Guid.NewGuid()){Code = "C222", SerialNumber = "SS722", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C223", SerialNumber = "SS723", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C224", SerialNumber = "SS724", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
                new Device(Guid.NewGuid()){Code = "C225", SerialNumber = "SS725", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C226", SerialNumber = "SS726", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C227", SerialNumber = "SS727", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C228", SerialNumber = "SS728", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C221", SerialNumber = "SS821", Status = EDeviceStatus.Actived, MacAddress ="00:E0:4C:7D:9C:B2", Type = EDeviceType.Cabinet},
                new Device(Guid.NewGuid()){Code = "C222", SerialNumber = "SS822", Status = EDeviceStatus.Blocked, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C223", SerialNumber = "SS823", Status = EDeviceStatus.Inatived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
                new Device(Guid.NewGuid()){Code = "C224", SerialNumber = "SS824", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C225", SerialNumber = "SS825", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C226", SerialNumber = "SS826", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
                new Device(Guid.NewGuid()){Code = "C227", SerialNumber = "SS827", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C228", SerialNumber = "SS828", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C231", SerialNumber = "SS921", Status = EDeviceStatus.Actived, MacAddress ="00:E0:4C:7D:9C:B2", Type = EDeviceType.LanPC},
                new Device(Guid.NewGuid()){Code = "C232", SerialNumber = "SS922", Status = EDeviceStatus.Blocked, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C233", SerialNumber = "SS923", Status = EDeviceStatus.Inatived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C234", SerialNumber = "SS924", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C235", SerialNumber = "SS925", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Kiosk },
                new Device(Guid.NewGuid()){Code = "C236", SerialNumber = "SS926", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C237", SerialNumber = "SS927", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.Cabinet },
                new Device(Guid.NewGuid()){Code = "C238", SerialNumber = "SS928", Status = EDeviceStatus.Actived, MacAddress = "00:E0:4C:1A:45:C2", Type = EDeviceType.LanPC },
            };

            var deviceRepository = new DeviceRepository(_dbContext);
            foreach (var deviceSeed in deviceSeeds)
            {
                count++;
                deviceRepository.Upsert(deviceSeed, _ => _.Code == deviceSeed.Code, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER

        }
    }
}
