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
using NLog;
using qodeless.domain.Model;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Account
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class PlayController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IPlayAppService _PlayAppService;

        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public PlayController(ApplicationDbContext db, IPlayAppService PlayAppService)
            : base(db)
        {
            _PlayAppService = PlayAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _PlayAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PlayViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _PlayAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Account por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<PlayViewModel> GetById(Guid id)
        {
            return  _PlayAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PlayViewModel>> GetAccounts()
        {
            return  _PlayAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] PlayViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _PlayAppService.Update(vm));
        }

        /// <summary>
        /// Retorna registros FK de Device
        /// ComboBox = DropDown List
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDevicesCmb")]
        public async Task<IEnumerable<EnumTypeVm>> GetDevicesCmb()
        {
            return _PlayAppService.GetDevices().Select(c => new EnumTypeVm(c.Code, c.Id));
        }
        /// <summary>
        /// Retorna registros FK de Device
        /// ComboBox = DropDown List
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/sites")]
        public async Task<IEnumerable<PlayViewModel>> GetPlayBySiteId(Guid id)
        {
            var user = GetLoggedUser();
            return _PlayAppService.GetBySiteId(id,user.UserId);
        }

        [HttpGet("GameRanking")]
        public async Task<IEnumerable<GameRankingViewModel>> GetGameRanking()
        {
            var data = _PlayAppService.GetRankingGames();
            if (!data.Any())
            {
                throw new Exception("NOT FOUND DATA");
            }
            return data;
        }

    }
}