using qodeless.domain.Entities.ACL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Enums;
using qodeless.domain.Enums.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.services.WebApi.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using Microsoft.AspNetCore.Identity;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.domain.Model;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Account
    /// </summary>
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    public class AccountController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IAccountAppService _accountAppService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public AccountController(ApplicationDbContext db,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<ApplicationUser> userManager,
                                IAccountAppService AccountAppService,
                                IHubContext<QodelessHub> hubContext)
                                : base(db, hubContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountAppService = AccountAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(_accountAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AccountViewModel vm)
        {
            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                Enabled = true,
                PhoneNumber = vm.CellPhone,
                CreationDate = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, "@rg!12345");//TODO: débito tecnico adrian

            if (!result.Succeeded)
            {
                return Response(false);
            }
            if (!_roleManager.RoleExistsAsync(Role.PARTNER).Result)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(Role.PARTNER.ToUpper()));
            }
            await _userManager.AddToRoleAsync(user, Role.PARTNER);

            vm.UserId = _userManager.FindByEmailAsync(user.Email).Result.Id;

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_accountAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Account por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<AccountViewModel> GetById(Guid id)
        {
             return _accountAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AccountViewModel>> GetAccounts()
        {
            return _accountAppService.GetAll();
        }

        [HttpGet("{id}/Sites")]
        public async Task<IEnumerable<SiteViewModel>> GetSites(Guid id)
        {
            return _accountAppService.GetSites(id);
        }

        [HttpGet("BlockAccount")]
        public async Task<IActionResult> UpdateBlockAccount(Guid id)
        {
            return CustomResponse(_accountAppService.UpdateBlockAccount(id));
        }

        [HttpGet("UnblockAccount")]
        public async Task<IActionResult> UpdateUnlockAccount(Guid id)
        {
            return CustomResponse(_accountAppService.UpdateUnlockAccount(id));
        }

        /// <summary>
        /// Retorna lista de Enums de Status
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStatusEnums")]
        public async Task<IEnumerable<EnumTypeVm>> GetStatusEnums()
        {
            return new List<EnumTypeVm>() {
                new EnumTypeVm("Ativo",Convert.ToString((int)EAccountStatus.Actived)),
                new EnumTypeVm("Inativo",Convert.ToString((int)EAccountStatus.Inatived)),
                new EnumTypeVm("Banido",Convert.ToString((int)EAccountStatus.Blocked)),
            };
        }

        /// <summary>
        /// Retorna lista de Accounts que comecam com a letra R === MOFOs
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetContasLetrasR")]
        public async Task<IEnumerable<AccountViewModel>> GetContasLetrasR()
        {
            return _accountAppService.GetAllBy(c => c.Name.StartsWith("R"));
        }

        /// <summary>
        /// Retorna lista de Accounts que comecam com a letra Informada
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetContasStartWith")]
        public async Task<IEnumerable<AccountViewModel>> GetContasStartWith(string letter)
        {
            return _accountAppService.GetAllBy(c => c.Name.ToUpper().StartsWith(letter.ToUpper()));
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] AccountViewModel vm)
        {
            switch ((EAccountStatus)Convert.ToInt32(vm.Status))
            {
                case EAccountStatus.Actived:
                    UnlockGameHub();
                    break;
                case EAccountStatus.Inatived:
                case EAccountStatus.Blocked:
                    LockGameHub();
                    break;
                default:
                    break;
            }

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_accountAppService.Update(vm));
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserPlaysRanking")]
        public async Task<IEnumerable<UserPlaysRankingViewModel>> GetUserPlaysRanking()
        {
            return _accountAppService.GetUserPlaysRanking();
        }

        /// <summary>
        /// Retorna lista com Ranking de Parceiros
        /// </summary>
        /// <returns></returns>
        [HttpGet("PartnersRaking")]
        public async Task<IEnumerable<PartnersRankingViewModel>> GetPartnersRanking()
        {
            return _accountAppService.GetPartnersRanking();
        }

        /// <summary>
        /// Retorna lista com Users Online
        /// </summary>
        /// <returns></returns>
        [HttpGet("OnlineUsers")]
        public async Task<IEnumerable<ActivityStatusViewModel>> GetOnlineUsers()
        {
            return _accountAppService.GetOnlineUsers();
        }

    }
}