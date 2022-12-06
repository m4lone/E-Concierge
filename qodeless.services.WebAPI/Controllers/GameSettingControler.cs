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
    ///  GameSetting
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class GameSettingController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IGameSettingAppService _gameSettingAppService;

        /// <summary>
        ///  GameSetting
        /// </summary>
        /// <param name="db"></param>
        public GameSettingController(ApplicationDbContext db, IGameSettingAppService GameSettingAppService)
            : base(db)
        {
            _gameSettingAppService = GameSettingAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _gameSettingAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar GameSetting
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GameSettingViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _gameSettingAppService.Add(vm));
        }

        /// <summary>
        /// Retorna GameSetting por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GameSettingViewModel> GetById(Guid id)
        {
            return  _gameSettingAppService.GetById(id);
        }

        /// <summary>
        /// Retorna GameSetting por Game ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByGameId/{id}")]
        public async Task<GameSettingViewModel> GetByGameId(Guid id)
        {
            return _gameSettingAppService.GetBy(x => x.GameId.Equals(id));
        }

        /// <summary>
        /// Retorna lista de GameSettings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<GameSettingViewModel> GetGameSettings()
        {
            var data = _gameSettingAppService.GetAll();
            return data; 
        }

        /// <summary>
        /// Atualiza GameSetting
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GameSettingViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _gameSettingAppService.Update(vm));
        }

    }
}