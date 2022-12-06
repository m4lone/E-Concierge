using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Enums.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Site
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize]
    public class SiteGameController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISiteGameAppService _siteGameAppService;

        /// <summary>
        ///  Site
        /// </summary>
        /// <param name="db"></param>
        public SiteGameController(ApplicationDbContext db, ISiteGameAppService siteGameAppService)
            : base(db)
        {
            _siteGameAppService = siteGameAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _siteGameAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Site
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SiteGameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _siteGameAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Site por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<SiteGameViewModel> GetById(Guid id)
        {
            return  _siteGameAppService.GetById(id)    ;
        }

        /// <summary>
        /// Retorna lista de Sites  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<SiteGameViewModel> GetSiteGames()
        {
            return _siteGameAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Site
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] SiteGameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _siteGameAppService.Update(vm));
        }
        /// <summary>
        /// Atualiza jogos a de uma casa
        /// </summary>
        /// <returns></returns>
        [HttpPut("GamesSite")]
        public async Task<IActionResult> GamesSite([FromBody] SiteGameMutipleVm vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_siteGameAppService.UpdateGameBySiteId(vm));
        }
        /// <summary>
        /// Retorna registros de jogos por site id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/Sites")]
        public IEnumerable<SiteGameViewModel> GetGamesBySiteId(Guid id)
        {
            return _siteGameAppService.GetSiteGame(id);
        }
    }
}