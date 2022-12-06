using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Account
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class GameController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IGameAppService _gameAppService;

        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public GameController(ApplicationDbContext db, IGameAppService GameAppService)
            : base(db)
        {
            _gameAppService = GameAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _gameAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _gameAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Game por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GameViewModel> GetById(Guid id)
        {
            return  _gameAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Games
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<GameViewModel> GetGames()
        {
            var data = _gameAppService.GetAll();
            return data; 
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _gameAppService.Update(vm));
        }

        /// <summary>
        /// Retorna ranking de Games mais lucrativos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ProfitRanking")]
        public IEnumerable<ProfitRankingViewModel> GetProfitRanking()
        {
            var data = _gameAppService.GetRankingGames();
            return data;
        }
    }
}