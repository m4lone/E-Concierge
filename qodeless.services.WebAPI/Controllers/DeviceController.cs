using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Device
    /// </summary>
    [Route("api/[controller]")]
    public class DeviceController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IDeviceAppService _deviceAppService;

        /// <summary>
        ///  Device
        /// </summary>
        /// <param name="db"></param>
        public DeviceController(ApplicationDbContext db, IDeviceAppService DeviceAppService)
            : base(db)
        {
            _deviceAppService = DeviceAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _deviceAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar Device
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DeviceViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _deviceAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Device por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<DeviceViewModel> GetById(Guid id)
        {
            return  _deviceAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Devices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<DeviceViewModel>> GetDevices()
        {
            return _deviceAppService.GetAll().OrderBy(_ => _.Code).ThenBy(_ => _.SerialNumber);
        }
        /// <summary>
        /// Retorna lista de Devices por site ID incluindo informações dos jogos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDevicesBySiteWithGames")]
        public async Task<IEnumerable<DeviceGame>> GetDevicesBySiteWithGames(Guid siteId)
        {
            return _deviceAppService.GetDevicesBySiteIdWithGames(siteId);
        }

        /// <summary>
        /// Atualiza Device
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] DeviceViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _deviceAppService.Update(vm));
        }

        [HttpGet("GetTotalDevices")]
        public int GetTotalDevices()
        {
            return _deviceAppService.GetTotalDevices();
        }

        /// <summary>
        /// Retorna lista com Status de Devices
        /// Atualiza Device
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckDeviceStatus")]
        public async Task<IEnumerable<DeviceStatusViewModel>> CheckDeviceStatus()
        {
            return _deviceAppService.GetDeviceStatus();
        }

        /// <summary>
        /// Retorna lista com Ranking de Devices
        /// Atualiza Device
        /// </summary>
        /// <returns></returns>
        [HttpGet("DevicesRanking")]
        public async Task<IEnumerable<DeviceRankingViewModel>> GetDevicesRanking()
        {
            return _deviceAppService.GetDevicesRanking();
        }

        /// Retorna lista de Devices ativos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActiveDevices")]
        public async Task<IEnumerable<DeviceViewModel>> GetActiveDevices()
        {
            return _deviceAppService.CheckActiveDevices();
        }
    }
}