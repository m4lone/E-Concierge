using qodeless.domain.Entities.ACL;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace qodeless.DataManager.Seeds
{
    public class ACLSeeder : SeederBase
    {
        public static void Seed(ApplicationDbContext _dbContext)
        {
            var userRoles = new List<UserRoleVm>()
            {
                new UserRoleVm(Role.SUPER_ADMIN           ,Guid.NewGuid().ToString(),  "eliel.qodeless@outlook.com"),
                new UserRoleVm(Role.SUPER_ADMIN           ,Guid.NewGuid().ToString(),  "barba@rgdigital.com.br"),
                new UserRoleVm(Role.SUPER_ADMIN           ,Guid.NewGuid().ToString(),  "torres@rgdigital.com.br"),
                new UserRoleVm(Role.PARTNER               ,Guid.NewGuid().ToString(),  "contaabc@rgdigital.com.br"),
                new UserRoleVm(Role.PARTNER               ,Guid.NewGuid().ToString(),  "contario@rgdigital.com.br"),
                new UserRoleVm(Role.SITE_ADMIN            ,Guid.NewGuid().ToString(),  "bardoabc@rgdigital.com.br"),
                new UserRoleVm(Role.SITE_ADMIN            ,Guid.NewGuid().ToString(),  "bardorio@rgdigital.com.br"),
                new UserRoleVm(Role.MAINTENANCE           ,Guid.NewGuid().ToString(),  "tecnicoabc@rgdigital.com.br"),
                new UserRoleVm(Role.MAINTENANCE           ,Guid.NewGuid().ToString(),  "tecnicorio@rgdigital.com.br"),
                new UserRoleVm(Role.SITE_COUNTER          ,Guid.NewGuid().ToString(),  "leituristaabc@rgdigital.com.br"),
                new UserRoleVm(Role.SITE_COUNTER          ,Guid.NewGuid().ToString(),  "leituristario@rgdigital.com.br"),
                new UserRoleVm(Role.SITE_ADMIN            ,Guid.NewGuid().ToString(),  "natalia.qodeless@outlook.com"),
                new UserRoleVm(Role.MAINTENANCE           ,Guid.NewGuid().ToString(),  "jeff.qodeless@outlook.com"),
                new UserRoleVm(Role.PARTNER               ,Guid.NewGuid().ToString(),  "cintia.qodeless@outlook.com"),
                new UserRoleVm(Role.PLAYER                ,Guid.NewGuid().ToString(),  "11993055326@rgdigital.com.br"),
            };
            InsertUsers(_dbContext, userRoles);

            var roleClaims = LoadClaimsFromFile(Directory.GetCurrentDirectory());

            InsertRoleClaims(_dbContext,roleClaims);
        }                
    }
}
