using qodeless.domain.Entities.ACL;
using Microsoft.AspNetCore.Mvc;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Enums;
using qodeless.domain.Enums.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Microsoft.AspNetCore.Identity;
using qodeless.services.WebApi.Model;
using qodeless.domain.Model;
using qodeless.Infra.CrossCutting.Identity.Entities;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Site
    /// </summary>
    [Route("api/[controller]")]
    public class SiteController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISiteAppService _siteAppService;
        private readonly IAccountAppService _accountAppService;
        private readonly ISitePlayerAppService _sitePlayerAppService;
        private readonly IGeoAppService _geoAppService;
        private readonly IInviteAppService _inviteAppService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly URLSettings _urlSetup;

        /// <summary>
        ///  Site
        /// </summary>
        /// <param name="db"></param>
        public SiteController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db,
            ISiteAppService siteAppService,
            IAccountAppService accountAppService,
            ISitePlayerAppService sitePlayerAppService,
            IGeoAppService geoAppService,
            IInviteAppService inviteAppService)
            : base(db)
        {
            _geoAppService = geoAppService;
            _siteAppService = siteAppService;
            _sitePlayerAppService = sitePlayerAppService;
            _inviteAppService = inviteAppService;
            _roleManager = roleManager;
            _userManager = userManager;
            _accountAppService = accountAppService;
        }
        [HttpGet("Example")]
        //Colocar um break point e analisar os valores retornados
        public async Task<IActionResult> Example()
        {
            //pega endereço por cep
            var a1 = _geoAppService.GetFullAddressByZipCode("92440236");
            //pega cep por endereço
            var a2 = _geoAppService.GetZipCodeByAddress("RS", "Porto Alegre", "Domingos Jose");
            //pega endereço por lat e lng
            var a3 = _geoAppService.GetAddressByLatLng((decimal)-29.97392560637615, (decimal)-51.19485161601512);
            //pega todas informações referente a um endereço
            var a4 = _geoAppService.GetByFullAddress(6, "Guajuviras", "RS", "Canoas", "rua 56b");
            //pega lat e lng de um por endereço
            var a5 = _geoAppService.GetLatLngByAddress(6, "Guajuviras", "RS", "Canoas", "rua 56b");
            return CustomResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(_siteAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Site
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SiteViewModel vm)
        {
            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                FullName = vm.Name,
                Enabled = true,
                PhoneNumber = vm.CellPhone,
                CreationDate = DateTime.Now
            };

            var password = _siteAppService.CreatePassword(9);
            var result = await _userManager.CreateAsync(user, password);//TODO: débito tecnico adrian
            var userId = _userManager.FindByEmailAsync(user.Email).Result.Id;
            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, Role.SITE_ADMIN);
            vm.UserId = userId;
            vm.Id = Guid.Parse(userId);
            vm.AccountId = vm.AccountId;

            var success = !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_siteAppService.Add(vm));
            if (success == null)
            {
                return BadRequest("FAILED TO CREATE USER");
            }

            await Task.Run(() =>
            {
                SendMail(Guid.Parse(userId), to: vm.Email, subject: "Senha de Acesso RG Digital",
                    body:
                        $"Essa é sua senha de acesso RG Digital : {password}. Por favor, Redefina sua senha <LINK PARA REDEFINIR PASSWORD (NÃO EXISTENTE AINDA) >clique aqui</a>",
                    cc: "",
                    bcc: ""
                   );
            });

            return Ok(new { success = true, password = password });
        }

        /// <summary>
        /// Retorna Site por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<SiteViewModel> GetById(Guid id)
        {
            return _siteAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Sites
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SiteViewModel>> GetSites()
        {
            return _siteAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Site
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] SiteViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_siteAppService.Update(vm));
        }
        /// <summary>
        /// Atualiza Moedas disponiveis para o Site
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateCurrencyGame")]
        public async Task<IActionResult> UpdateCurrencyGame([FromBody] CurrencyGameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_siteAppService.UpdateCurrencyGameBySiteId(vm));
        }
        /// <summary>
        /// Retorna Currency vinculados a um site por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrencyGameById/{id}")]
        public async Task<List<int>> GetCurrencyGameById(Guid id)
        {
            return _siteAppService.GetCurrencyGame(id);
        }
        /// <summary>
        /// Retorna lista de Enums de SiteType
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSiteTypeEnums")]
        public async Task<IEnumerable<EnumTypeVm>> GetSiteTypeEnums()
        {
            return new List<EnumTypeVm>() {
                new EnumTypeVm("Cassino",Convert.ToString((int)ESiteType.Casino)),
                new EnumTypeVm("Online",Convert.ToString((int)ESiteType.Cloud)),
                new EnumTypeVm("Padrão",Convert.ToString((int)ESiteType.Default)),
                new EnumTypeVm("Lan House",Convert.ToString((int)ESiteType.LanHouse)),
                new EnumTypeVm("Outros",Convert.ToString((int)ESiteType.Other)),
            };
        }

        /// <summary>
        /// Retorna registros FK de Account
        /// ComboBox = DropDown List
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAccountsCmb")]
        public async Task<IEnumerable<EnumTypeVm>> GetAccountsCmb()
        {
            return _siteAppService.GetAccounts().Select(c => new EnumTypeVm(c.Name, c.Id));
        }
        /// <summary>
        /// fator segurança
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByCode/{code}")]
        public async Task<IActionResult> GetGamesByCode(string code)
        {
            return Redirect($"https://google.com.br");
        }
        /// <summary>
        /// Retorna Objeto para unity montar o lobby
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByCode/{code}/{playerId}")]
        public async Task<IActionResult> GetGamesByCode(string code, string playerId)
        {
            var site = (_siteAppService.GetAllBy(c => c.Code == code)).FirstOrDefault();

            if (site == null)
            {
                return Redirect($"https://google.com.br");
            }
            var sitePlayer = _sitePlayerAppService.GetBySiteIdPlayerId(site.Id, playerId);

            if (sitePlayer == null)
            {
                _sitePlayerAppService.Add(new SitePlayerViewModel()
                {
                    SiteId = site.Id,
                    UserPlayerId = playerId
                });

                return Ok(GetGamesPlayerById(playerId, site.Id));
            }
            _sitePlayerAppService.Update(sitePlayer);
            return Ok(GetGamesPlayerById(playerId, site.Id));
        }
        /// <summary>
        /// Retorna Objeto para unity montar o lobby por invite
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByInvite/{inviteToken}/{playerId}")]
        public async Task<IActionResult> GetGamesByInvite(string inviteToken, string playerId)
        {
            var token = _inviteAppService.GetBy(c => c.InviteToken == inviteToken);
            var site = _siteAppService.GetBy(c => c.Id == token.SiteId);
            if (token.IsActive)
            {
                if (site == null)
                {
                    return Redirect($"https://google.com.br");
                }

                token.IsActive = false;

                var sitePlayer = _sitePlayerAppService.GetBySiteIdPlayerId(site.Id, playerId);

                if (sitePlayer == null)
                {
                    _sitePlayerAppService.Add(new SitePlayerViewModel()
                    {
                        SiteId = site.Id,
                        UserPlayerId = playerId
                    });
                    _inviteAppService.Update(token);
                    return Ok(GetGamesPlayerById(playerId, site.Id));
                }
                _inviteAppService.Update(token);
                _sitePlayerAppService.Update(sitePlayer);
                return Ok(GetGamesPlayerById(playerId, site.Id));
            }
            return BadRequest();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalSites")]
        public SiteViewModel GetTotalSites()
        {
            var totalSites = _siteAppService.GetTotalSites();
            return totalSites;
        }

        /// <summary>
        /// Retorna lista com Ranking de Sites
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSitesRanking")]
        public async Task<IEnumerable<SiteRankingViewModel>> GetSitesRanking()
        {
            var totalSites = _siteAppService.GetSitesRanking();
            return totalSites;
        }

        #region Collaborator
        /// <summary>
        /// Listar Operadores por site ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOperators/{employerId}")]
        public async Task<IEnumerable<SiteOperatorViewModel>> GetOperators(Guid? employerId)
        {
            return _siteAppService.GetOperators(employerId);
        }

        /// <summary>
        /// Retorna o Operador por Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOperator/{id}")]
        public async Task<SiteOperatorViewModel> GetOperator(string id)
        {
            var identityUser = _userManager.Users.FirstOrDefault(x => x.Id.Equals(id));
            var siteName = Db.Sites.Where(x => x.Id == identityUser.EmployerId).Select(x => x.Name).FirstOrDefault();

            return new SiteOperatorViewModel
            {
                UserId = identityUser.Id,
                Email = identityUser.Email,
                Cpf = identityUser.Cpf,
                SiteName = siteName,
                FullName = identityUser.FullName,
                PhoneNumber = identityUser.PhoneNumber
            };
        }

        /// <summary>
        /// Deleta o Operador
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteCollaborator/{id}")]
        public async Task<IActionResult> DeleteCollaborator(string id)
        {
            var identityUser = _userManager.Users.FirstOrDefault(x => x.Id.Equals(id));
            var result = _userManager.DeleteAsync(identityUser);

            if (!result.IsCompleted)
            {
                return Response(result.Result);
            }
            return Response(true);
        }

        /// <summary>
        /// Adicionar colaborador (Operador) do site
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddCollaborator")]
        public async Task<IActionResult> AddCollaborator([FromBody] RegisterCollaboratorViewModel vm)
        {
            Random randNum = new Random();
            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                Enabled = true,
                PhoneNumber = vm.PhoneNumber,
                CreationDate = DateTime.Now,
                Cpf = vm.Cpf,
                EmployerId = vm.EmployerId,
                FullName = vm.FullName,
            };
            //cria usuario no banco (aspnetuser)
            var result = await _userManager.CreateAsync(user, "!Asdf1904");//TODO: débito tecnico adrian, Implementar email para resetar senha

            if (!result.Succeeded)
            {
                return Response(false);
            }
            if (!_roleManager.RoleExistsAsync(Role.SITE_OPERATOR).Result)
            {
                await _roleManager.CreateAsync(new IdentityRole(Role.SITE_OPERATOR.ToUpper()));
            }
            result = await _userManager.AddToRoleAsync(user, Role.SITE_OPERATOR);
            if (!result.Succeeded)
            {
                return Response(false);
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return Response(true);
        }

        /// <summary>
        /// Atualiza o colaborador (Operador) do site
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateCollaborator")]
        public async Task<IActionResult> UpdateCollaborator([FromBody] UpdateCollaboratorViewModel vm)
        {
            var identityUser = _userManager.Users.FirstOrDefault(x => x.Id.Equals(vm.UserId));
            identityUser.FullName = vm.FullName;
            identityUser.Cpf = vm.Cpf;
            identityUser.PhoneNumber = vm.PhoneNumber;

            var result = await _userManager.UpdateAsync(identityUser);

            if (!result.Succeeded)
            {
                return Response(result.Errors);
            }
            return Response(true);
        }
        #endregion
    }
}