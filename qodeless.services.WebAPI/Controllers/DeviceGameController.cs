using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.Interfaces;
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
    [Authorize]
    public class DeviceGameController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IDeviceGameAppService _DeviceGameAppService;

        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public DeviceGameController(ApplicationDbContext db, IDeviceGameAppService DeviceGameAppService)
            : base(db)
        {
            _DeviceGameAppService = DeviceGameAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _DeviceGameAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DeviceGameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _DeviceGameAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Account por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DeviceGameViewModel> GetById(Guid id)
        {
            return  _DeviceGameAppService.GetById(id);
        }

        /// <summary>
        /// Retorna games por device ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/game")]
        public async Task <DeviceGameViewModel> GetGameByDeviceId(Guid id)
        {
            return _DeviceGameAppService.GetGameByDeviceId(id);
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<DeviceGameViewModel>> GetAccounts()
        {
            return  _DeviceGameAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] DeviceGameViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _DeviceGameAppService.Update(vm));
        }
    }
}