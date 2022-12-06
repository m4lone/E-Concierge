using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
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
    public class AccountGameController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IAccountGameAppService _AccountGameAppService;

        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public AccountGameController(ApplicationDbContext db, IAccountGameAppService AccountGameAppService)
            : base(db)
        {
            _AccountGameAppService = AccountGameAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _AccountGameAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AccountGameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _AccountGameAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Account por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<AccountGameViewModel> GetById(Guid id)
        {
            return  _AccountGameAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AccountGameViewModel>> GetAccounts()
        {
            return  _AccountGameAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] AccountGameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _AccountGameAppService.Update(vm));
        }

        /// <summary>
        /// retorna lista de jogos relacionados ao account ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetGamesByAccountId/{id}")]
        public async Task<IEnumerable<Game>> GetGamesByAccountId(Guid id)
        {
            return  _AccountGameAppService.GetGamesByAccountId(id);
        }
        
        /// <summary>
        /// Adicionar Account x games
        /// </summary>
        /// <returns></returns>
        [HttpPost("GamesAccount")]
        public async Task<IActionResult> UpdateGamesByAccountId([FromBody] AccountGameMutiplevm vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _AccountGameAppService.UpdateGamesByAccountId(vm));
        }
    }
}





      