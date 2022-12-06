using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Account
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize]
    public class SessionSiteController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISessionSiteAppService _SessionSiteAppService;

        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public SessionSiteController(ApplicationDbContext db, ISessionSiteAppService SessionSiteAppService)
            : base(db)
        {
            _SessionSiteAppService = SessionSiteAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _SessionSiteAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SessionSiteViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _SessionSiteAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Account por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<SessionSiteViewModel> GetById(Guid id)
        {
            return  _SessionSiteAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SessionSiteViewModel>> GetAccounts()
        {
            return  _SessionSiteAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] SessionSiteViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _SessionSiteAppService.Update(vm));
        }
    }
}