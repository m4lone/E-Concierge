using jll.portal_api.services.WebAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.DataManager.Seeds;
using qodeless.domain.Entities.ACL;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.services.api.SysCobrancaClient.Managers.Zenvia;
using qodeless.services.WebApi.Model;
using qodeless.services.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using qodeless.application;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace qodeless.services.WebApi.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ACLController : ApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenAppService _tokenAppService;
        private readonly IInviteAppService _inviteAppService;
        private readonly ISitePlayerAppService _sitePlayerAppService;

        public ACLController(
           RoleManager<IdentityRole> roleManager,
           UserManager<ApplicationUser> userManager,
          ApplicationDbContext db, IWebHostEnvironment env, ITokenAppService tokenAppService, IInviteAppService inviteAppService, ISitePlayerAppService sitePlayerAppService) : base(db, null, env)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenAppService = tokenAppService;
            _inviteAppService = inviteAppService;
            _sitePlayerAppService = sitePlayerAppService;
        }
        /// <summary>
        /// Adicionar Role apenas Teste
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddRole")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.PLC01)]
        public async Task<IActionResult> AddRole([FromBody] RoleViewModel vm)
        {
            if (vm.Role.ToUpper().IsValidRole())
            {
                if (!_roleManager.RoleExistsAsync(vm.Role).Result)
                {
                    await _roleManager.CreateAsync(new IdentityRole(vm.Role.ToUpper()));
                }
                var role = await _roleManager.FindByNameAsync(vm.Role);
                //vincula claims no role (aspnetrolesclaims)
                if (vm.Claims.Count > 0)
                {
                    foreach (var claim in vm.Claims)
                    {
                        await _roleManager.AddClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));
                    }
                }
                return Response(true);
            }
            return Response(false);
        }

        /// <summary>
        /// Lista Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("Users")]
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return Db.Users.ToList();

        }

        [HttpGet("Players")]
        public async Task<List<IdentityUser<string>>> GetPlayers()
        {
            var roles = Db.Roles.Where(x => x.Name.ToUpper() == Role.PLAYER).FirstOrDefault();
            var players = Db.UserRoles.Where(_ => _.RoleId == roles.Id).ToList();

            var playersList = new List<IdentityUser<string>>();

            foreach(var player in players)
            {
                var dadosPlayers = Db.Users.Where(_ => _.Id == player.UserId).FirstOrDefault();

                if(dadosPlayers != null)
                {
                    playersList.Add(dadosPlayers);
                }
            }

            return playersList;
        }

        [HttpPut("UpdatePlayerLockoutEnebledById")]
        public async Task<IActionResult> UpdatePlayer([FromBody] UserLockoutViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.UserId);
            user.LockoutEnabled = vm.Lockout;
            await _userManager.UpdateAsync(user);
            return Response($"state updated for {user.LockoutEnabled}");
        }

        /// <summary>
        /// Lista Role
        /// </summary>
        /// <returns></returns>
        [HttpGet("Roles")]
        public async Task<List<IdentityRole>> GetRoles()
        {
            return Db.Roles.ToList();
        }

        /// <summary>
        /// Lista UserRoles
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserRoles")]
        public async Task<List<IdentityUserRole<string>>> GetUserRoles()
        {
            return Db.UserRoles.ToList();
        }

        /// <summary>
        /// Lista Role
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserClaims")]
        public async Task<List<IdentityUserClaim<string>>> GetUserClaims()
        {
            return Db.UserClaims.ToList();
        }

        [HttpGet("UserClaimsPlayers")]
        public async Task<List<IdentityUserClaim<string>>> GetRoleClaims()
        {
            return Db.UserClaims.ToList();
        }

        /// <summary>
        /// Adicionar Usuário
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddUser")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.REGISTER_USER)]
        //[Authorize(Roles = Role.MANAGER)]
        public async Task<IActionResult> AddUser([FromBody] RegisterViewModel vm)
        {

            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                Enabled = true,
            };
            //cria usuario no banco (aspnetuser)
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                // User claim for write customers data

                //relaciona role existente com o usuario criado (aspnetuserroles)
                if (vm.Role.ToUpper().IsValidRole())
                {
                    if (!_roleManager.RoleExistsAsync(vm.Role).Result)
                    {
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole(vm.Role.ToUpper()));
                    }
                    await _userManager.AddToRoleAsync(user, vm.Role);
                }
                
                //relaciona claim com usuario (aspnetuserclaims)
                if (vm.Claims != null)
                {
                    if (vm.Claims.Count > 0)
                    {
                        foreach (var claim in vm.Claims)
                        {
                            await _userManager.AddClaimAsync(user, new Claim(claim.ClaimType, claim.ClaimValue));
                        }
                    }
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                return Response(true);
            }
            return Response(false);
        }

        /// <summary>
        /// Adicionar apenas Usuários jogadores (Unity)
        /// </summary>
        /// <returns></returns>
        [HttpPost("RequestToken")]
        public async Task<IActionResult> RequestToken([FromBody] TokenMobileViewModel vm)
        {
            var token = new TokenViewModel  (vm.Phone); // chamando construtor da classe
            var result = await _tokenAppService.Add(token);
            if (result.IsValid)
            {
                try
                {
                    SmsSenderManager.SendSms(vm.Phone, token.Code);
                    WhatsappSenderManager.SendWhatsapp("5511945464365", token.Code);
                    return Response(token.Code);
                }
                catch (Exception)
                {
                }                
            }
                
            return Response(string.Empty);
        }

        /// <summary>
        /// Adicionar Role apenas Teste
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddRoleClaimsAsync")]
        public async Task<IActionResult> AddRoleClaimsAsync([FromBody] RoleViewModel vm)
        {
            var roleId = Guid.NewGuid().ToString();

            var role = Db.Roles.FirstOrDefault(c => c.Name == vm.Role);
            if (role == null)
            {
                Db.Roles.Add(new IdentityRole()
                {
                    Id = roleId,
                    Name = vm.Role,
                    NormalizedName = vm.Role.ToUpper()
                });
            }
            else
            {
                roleId = role.Id;
            }

            if (Db.SaveChanges() > 0)
            {
                foreach (var item in vm.Claims)
                {
                    Db.RoleClaims.Add(new IdentityRoleClaim<string>()
                    {
                        RoleId = roleId,
                        ClaimType = item.ClaimType,
                        ClaimValue = item.ClaimValue
                    });
                    Db.SaveChanges();
                }
            }
            return Response(false);
        }

        /// <summary>
        /// Adicionar apenas Usuários jogadores (Unity)
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddGameUser")]
        public async Task<IActionResult> AddGameUser([FromBody] RegisterMobileViewModel vm)
        {
            var generatedEmail = $"{vm.Phone}@rgdigital.com.br";

            var token = (await _tokenAppService.GetAllBy(c => c.Phone == vm.Phone && c.DtExpiration >= DateTime.Now.AddMinutes(-3))).FirstOrDefault();
            if (token != null)
            {
                var user = new ApplicationUser
                {
                    UserName = generatedEmail,
                    Email = generatedEmail,
                    Enabled = true,
                };

                //cria usuario no banco (aspnetuser)
                var result = await _userManager.CreateAsync(user, "!Asdf1904"); // Technical Debt
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.PLAYER);
                    var roleClaims = SeederBase.LoadClaimsFromFile(_env.WebRootPath);
                    foreach (var policy in roleClaims.Where(c => c.Role == Role.PLAYER))
                    {
                        foreach (var claim in policy.Claims)
                        {
                            await _userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
                        }
                    }

                    return Response(user.Id);
                }
            }

            return Response(string.Empty);
        }
    
        /// <summary>
        /// Adicionar apenas Usuários jogadores (Unity)
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddGameUserByToken")]
        public async Task<IActionResult> AddGameUserByToken([FromBody] RegisterTokenViewModel vm)
        {
            var generatedEmail = $"{vm.Phone}@rgdigital.com.br";

            var token = _inviteAppService.GetAllBy(c => c.InviteToken == vm.Token
                                                && !c.IsActive // Nunca utilizados
                                                && c.DtExpiration >= DateTime.Now.AddDays(-1) // Invite de 1 dia
                                                ).FirstOrDefault();
            if (token != null)
            {
                var user = new ApplicationUser
                {
                    UserName = generatedEmail,
                    Email = generatedEmail,
                    Enabled = true,
                };

                //cria usuario no banco (aspnetuser)
                var result = await _userManager.CreateAsync(user, "!Asdf1904"); // Technical Debt
                if (result.Succeeded)
                {
                    //vincula o jogador ao site
                    var resultValidator =  _sitePlayerAppService.Add(new SitePlayerViewModel() { SiteId = vm.SiteId, UserPlayerId = user.Id });
                    if (resultValidator.IsValid)
                    {

                        //vincula o jogador ao role Player
                        await _userManager.AddToRoleAsync(user, Role.PLAYER);
                        var roleClaims = SeederBase.LoadClaimsFromFile(_env.WebRootPath);
                        foreach (var policy in roleClaims.Where(c => c.Role == Role.PLAYER))
                        {
                            foreach (var claim in policy.Claims)
                            {
                                await _userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
                            }
                        }
                    }
                    return Response(user.Id);
                }
            }

            return Response(string.Empty);
        }


        /// <summary>
        /// Adicionar Role apenas Teste
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddRoleAsync")]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleViewModelEx vm)
        {
            var result = Db.Roles.Add(new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = vm.Name,
                NormalizedName = vm.Name.ToUpper()
            });

            if (Db.SaveChanges() > 0)
            {
                return Response(true);
            }
            return Response(false);
        }
    }

    public class RoleViewModelEx
    {
        public string Name { get; set; }
    }
}

