using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qodeless.domain.Entities.ACL;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Enums.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qodeless.Infra.CrossCutting.Identity.Entities;
using NLog;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Account
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize]
    public class SitePlayerController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISitePlayerAppService _SitePlayerAppService;

        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public SitePlayerController(ApplicationDbContext db, ISitePlayerAppService SitePlayerAppService)
            : base(db)
        {
            _SitePlayerAppService = SitePlayerAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _SitePlayerAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SitePlayerViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _SitePlayerAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Account por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<SitePlayerViewModel> GetById(Guid id)
        {
            return  _SitePlayerAppService.GetById(id);
        }

        [HttpGet("Players")]
        public async Task<List<ApplicationUser>> GetPlayers()
        {
            return  Db.Users.ToList();
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SitePlayerViewModel>> GetAccounts()
        {
            return  _SitePlayerAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] SitePlayerViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _SitePlayerAppService.Update(vm));
        }

        /// <summary>
        /// Retorna registros FK de Device
        /// ComboBox = DropDown List
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSitesCmb")]
        public async Task<IEnumerable<EnumTypeVm>> GetSitesCmb()
        {
            return _SitePlayerAppService.GetSites().Select(c => new EnumTypeVm(c.Name, c.Id));
        }

        /// <summary>
        /// Retorna lista de Player com siteId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/Sites")]
        public IEnumerable<SitePlayerViewModel> GetIdPlayer(Guid id)
        {
            return _SitePlayerAppService.GetPlayer(id);
        }
    }
}