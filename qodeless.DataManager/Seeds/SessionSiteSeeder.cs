using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.DataManager.Seeds
{
    public class SessionSiteSeeder :SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {

            var Expire_DT = new DateTime(2021, 12, 31, 23, 59, 59);

            #region DEVICESEEDER
            const int TOTAL_ITEMS = 50;
            int count = 0;
            var sites = new SiteRepository(_dbContext).GetAll().ToList();
            var users = _dbContext.Users.ToList();
            var sessionSites = new List<SessionSite>();

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionSites.Add(new SessionSite(Guid.NewGuid()) { UserOperationId = GetFKRandomUser(users).ToString(), DtBegin = DateTime.Today, DtEnd = Expire_DT, Status = EStatusSessionSite.BillingClosed, SiteId = GetFKRandom(sites) });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionSites.Add(new SessionSite(Guid.NewGuid()) { UserOperationId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, Status = EStatusSessionSite.BillingOpen, SiteId = GetFKRandom(sites) });


            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionSites.Add(new SessionSite(Guid.NewGuid()) { UserOperationId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, Status = EStatusSessionSite.BillingProcess, SiteId = GetFKRandom(sites) });


            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionSites.Add(new SessionSite(Guid.NewGuid()) { UserOperationId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, Status = EStatusSessionSite.BusinessDayClosed, SiteId = GetFKRandom(sites) });

            for (int i = 0; i < TOTAL_ITEMS; i++)
                sessionSites.Add(new SessionSite(Guid.NewGuid()) { UserOperationId = GetFKRandomUser(users), DtBegin = DateTime.Today, DtEnd = Expire_DT, Status = EStatusSessionSite.BusinessDayOpen, SiteId = GetFKRandom(sites) });




            var sessionSiteRepository = new SessionSiteRepository(_dbContext);
            foreach (var sessionsite in sessionSites)
            {
                count++;
                sessionSiteRepository.Upsert(sessionsite, _ => _.SiteId == sessionsite.SiteId, true);
            }
            Console.WriteLine($"Itens salvos -> {count}");
            #endregion //DEVICESEEDER

        }
    }
}
