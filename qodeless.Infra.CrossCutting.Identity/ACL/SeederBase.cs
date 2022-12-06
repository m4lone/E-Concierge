using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
using qodeless.domain.Entities;
using qodeless.domain.Entities.ACL;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace qodeless.DataManager.Seeds
{

    public class SeederBase
    {
        #region SEEDER_IDS
        public static string ID_USER1 = "69dd235e-0ae9-4ea6-a8ac-96f30a12f462";
        public static string ID_USER2 = "6ca3bbd8-73ed-42cc-a22d-08a3022b5639";
        public static string ID_USER3 = "d8eb3853-7fe6-4ac9-b993-d24eab19395a";
        public static string ID_USER4 = "b6704a5b-a405-4ec0-a26f-dc5c815ebbab";
        public static string ID_USER5 = "75fa7783-fb17-48b8-a1f3-93b6af435387";

        public static Dictionary<string, string> ID_ROLES = new Dictionary<string, string>()
        {
            {Role.SUPER_ADMIN,            "5920f91a-79cf-477f-af6c-5ee5c32a600d"},
            {Role.SITE_ADMIN,             "61c66500-2e61-42fc-ab7e-64d456daf1d2"},
            {Role.MAINTENANCE,            "944fcee3-0796-4d9c-8d6e-84508802aea6"},
            {Role.PARTNER,                "ff429fb2-02fe-4e06-9716-dfbe3a094a34"},
            {Role.PLAYER,                 "44777328-fb46-4a0b-9d16-586cf9c5c5a4"},
            {Role.SITE_COUNTER,           "44777328-fb46-4a0b-9d16-586cf9c5c5b4"},
            {Role.FINANCE_METER,          "44777328-fb46-4a0b-9d16-586cf9c5c5c4"},
            {Role.SITE_OPERATOR,          "44777328-fb46-4a0b-9d16-586cf9c5c5d4"},

        };
        #endregion //SEEDER_IDS

        #region ACL_FUNCTIONS

        public static Guid GetFKRandom<TEntity>(List<TEntity> items) where TEntity : class
        {
            var index = new Random().Next(0, items.Count - 1);
            var item = items[index] as Entity;
            return item.Id;
        }

        public static string GetFKRandomUser(List<ApplicationUser> users)
        {
            var index = new Random().Next(0, users.Count - 1);
            var user = users[index];
            return user.Id;
        }

        protected static void InsertUsers(ApplicationDbContext _dbContext, List<UserRoleVm> userRoles)
        {
            foreach (var item in userRoles)
            {
                var role = _dbContext.Roles.FirstOrDefault(c => c.NormalizedName == item.Role.ToUpper() || c.Id == ID_ROLES[item.Role]);
                if (role == null)
                {
                    _dbContext.Roles.Add(new IdentityRole()
                    {
                        Id = ID_ROLES[item.Role],
                        Name = item.Role.ToUpper(),
                        NormalizedName = item.Role.ToUpper()
                    });

                    _dbContext.SaveChanges();
                }
            }

            foreach (var item in userRoles)
            {
                var role = _dbContext.Roles.FirstOrDefault(c => c.NormalizedName == item.Role.ToUpper());
                var user = _dbContext.Users.FirstOrDefault(c => c.Email == item.Email);
                if (user == null && role != null)
                {
                    _dbContext.Users.Add(GetNewUser(item.Id, item.Email));

                    if (_dbContext.SaveChanges() > 0)
                    {
                        _dbContext.UserRoles.Add(new IdentityUserRole<string>()
                        {
                            UserId = item.Id,
                            RoleId = role.Id
                        });
                    }
                }
            }
            _dbContext.SaveChanges();
        }

        protected static void InsertRoleClaims(ApplicationDbContext _dbContext, List<RoleClaimVm> roleClaimVms)
        {
            foreach (var item in roleClaimVms)
            {
                var role = _dbContext.Roles.FirstOrDefault(c => c.NormalizedName == item.Role.ToUpper());
                if (role != null)
                {
                    foreach (var claimItem in item.Claims.Where(_=>_.Status == EClaimStatus.Yes))
                    {
                        var oldClaim = _dbContext.RoleClaims.FirstOrDefault(c => c.RoleId == role.Id && c.ClaimType == claimItem.Type);
                        if (oldClaim != null)
                        {
                            _dbContext.RoleClaims.Remove(oldClaim);
                            _dbContext.SaveChanges();
                        }

                        _dbContext.RoleClaims.Add(new IdentityRoleClaim<string>
                        {
                            RoleId = role.Id,
                            ClaimType = claimItem.Type.ToUpper(),
                            ClaimValue = claimItem.Value.ToUpper()
                        });
                    }
                }
            }
            _dbContext.SaveChanges();
        }

        protected static ApplicationUser GetNewUser(string id, string email)
        {
            return new ApplicationUser()
            {
                Id = id.ToString(),
                Email = email,
                UserName = email,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = email.ToUpper(),
                Enabled = true,
                LockoutEnabled = false,
                CreationDate = DateTime.Now,
                PasswordHash = "AQAAAAEAACcQAAAAEIMnJ5rjHS11+94nSBJKoTR7k3O2sZYEiJdks22dD2hxFfbr+dbaVTrmM21SZmcyDA==", //!Asdf1904
                SecurityStamp = "FQHIGXG57MGW33C2QOHFIA35L6NZ5GSQ"
            };
        }

        protected static RoleClaimVm GetUserClaimVm(
            string policyName,
            string roleName, 
            string claimType,
            List<string> claimValueRange)
        {
            const int IDX_READ = 0;
            const int IDX_CREATE = 1;
            const int IDX_UPDATE = 2;
            const int IDX_DELTE = 3;
            return  new RoleClaimVm(policyName)
            {
                Role = roleName,
                Claims = new List<ClaimVm>()
                    {
                        new ClaimVm(claimType,ClaimValue.READ,GetStatus(claimValueRange,IDX_READ)),
                        new ClaimVm(claimType,ClaimValue.CREATE,GetStatus(claimValueRange,IDX_CREATE)),
                        new ClaimVm(claimType,ClaimValue.UPDATE,GetStatus(claimValueRange,IDX_UPDATE)),
                        new ClaimVm(claimType,ClaimValue.DELETE,GetStatus(claimValueRange,IDX_DELTE)),
                    }
            };
        }

        private static EClaimStatus GetStatus(List<string> values, int idx) => values[idx].ToUpper() == "S" ? EClaimStatus.Yes : EClaimStatus.No;

       

        public static List<RoleClaimVm> LoadClaimsFromFile(string rootPath)
        {
            const string ACL_TEMPLATE_FILE = "QODELESS_ACL_PROJETO_ADMINISTREI_20211022_2257.xlsx";
            const int PAGE_CLAIMS_IDX = 1;
            const int ROLES_QTTY = 8;
            const int BEGIN_COL = 3;
            const int BEGIN_ROW = 11;
            const int COL_CLAIM = 3;
            const int COL_ROLE = 4;
            const int COL_POLICY = 5;
            const int COL_READ = 6;
            const int COL_DELETE = 9;
            var claims = new List<RoleClaimVm>();
            var templatePath = Path.Combine(rootPath, "Files", ACL_TEMPLATE_FILE);
            FileInfo template = new FileInfo(templatePath);
            using (var package = new ExcelPackage(template))
            {
                var ws = package.Workbook.Worksheets.ElementAt(PAGE_CLAIMS_IDX);
                var cellEnd = ws.Cells.Where(_ => _.Value != null).FirstOrDefault(c => c.Value.ToString() == "END");
                var claimRange = ws.Cells[BEGIN_ROW, BEGIN_COL, cellEnd.Start.Row - 1, BEGIN_COL].Where(c => c.Value != null && !string.IsNullOrEmpty(c.Value.ToString())).ToList();
                foreach (var claimRow in claimRange)
                {
                    var currentRow = claimRow.Start.Row;
                    var roleRange = ws.Cells[currentRow, COL_ROLE, currentRow + (ROLES_QTTY - 1), COL_ROLE].ToList();
                    var claim = ws.Cells[currentRow, COL_CLAIM].Value.ToString();
                    foreach (var roleRow in roleRange)
                    {
                        var row = roleRow.Start.Row;
                        var col = roleRow.Start.Column;
                        var role = ws.Cells[row, COL_ROLE].Value.ToString();
                        var policy = ws.Cells[row, COL_POLICY].Value.ToString();
                        var actionsRange = ws.Cells[row, COL_READ, row, COL_DELETE].Select(c => c.Value.ToString()).ToList();
                        claims.Add(GetUserClaimVm(policy, role, claim, actionsRange));
                    }
                }

            }
            return claims;
        }

        #endregion //ACL_FUNCTIONS
    }

    public class UserRoleVm
    {
        public UserRoleVm(string role, string id, string email)
        {
            Id = id;
            Email = email;
            Role = role;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class ClaimVm
    {
        public ClaimVm(string type, string value, EClaimStatus status)
        {
            Type = type;
            Value = value;
            Status = status;
        }

        public string Type { get; set; }
        public string Value { get; set; }
        public EClaimStatus Status { get; set; }
    }

    public class RoleClaimVm
    {
        public RoleClaimVm(string policyName)
        {
            PolicyName = policyName;
        }

        public string PolicyName { get; set; }
        public string Role { get; set; }
        public List<ClaimVm> Claims { get; set; }
    }
}
