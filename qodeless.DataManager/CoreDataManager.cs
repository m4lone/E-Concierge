using qodeless.DataManager.Seeds;
using System;

namespace qodeless.DataManager
{
    public class CoreDataManager : DataManager
    {
  
        public override void Seed() => InjectAllData();
        

        public async void InjectAllData()
        {
            Console.WriteLine("ACLSeeder");
            ACLSeeder.Seed(_dbContext);
            Console.WriteLine("AccountSeeder");
            AccountSeeder.Seed(_dbContext);
            Console.WriteLine("SiteSeeder");
            SiteSeeder.Seed(_dbContext);
            Console.WriteLine("GameSeeder");
            GameSeeder.Seed(_dbContext);
            Console.WriteLine("AccountGameSeeder");
            AccountGameSeeder.Seed(_dbContext);
            Console.WriteLine("DeviceSeeder");
            DeviceSeeder.Seed(_dbContext);
            Console.WriteLine("SiteDeviceSeeder");
            SiteDeviceSeeder.Seed(_dbContext);
            Console.WriteLine("CurrencySeeder");
            CurrencySeeder.Seed(_dbContext);
            Console.WriteLine("SiteGameSeeder");
            SiteGameSeeder.Seed(_dbContext);
            Console.WriteLine("DeviceGameSeeder");
            DeviceGameSeeder.Seed(_dbContext);
            Console.WriteLine("TokenSeeder");
            TokenSeeder.Seed(_dbContext);
            Console.WriteLine("VirtualBalanceSeeder");
            VirtualBalanceSeeder.Seed(_dbContext);
            Console.WriteLine("VouscherSeeder");
            VouscherSeeder.Seed(_dbContext);
            Console.WriteLine("SuccessFeeSeeder");
            SuccessFeeSeeder.Seed(_dbContext);
            Console.WriteLine("SitePlayerSeeder");
            SitePlayerSeeder.Seed(_dbContext);
            Console.WriteLine("SessionDeviceSeeder");
            SessionDeviceSeeder.Seed(_dbContext);
            Console.WriteLine("SessionSiteSeeder");
            SessionSiteSeeder.Seed(_dbContext);
            Console.WriteLine("ExpenseSeeder");
            ExpenseSeeder.Seed(_dbContext);
            Console.WriteLine("ExpenseRequestSeeder");
            ExpenseRequestSeeder.Seed(_dbContext);
            Console.WriteLine("PlaySeeder");
            PlaySeeder.Seed(_dbContext);
        }
    }
}
