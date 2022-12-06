using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Site
    /// </summary>
    [Route("api/[controller]")]
    public class SiteDeviceController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISiteDeviceAppService _siteDeviceAppService;
        private readonly IDeviceAppService _deviceAppService;
        private readonly ISiteAppService _siteAppService;

        /// <summary>
        ///  Site
        /// </summary>
        /// <param name="db"></param>
        public SiteDeviceController(ApplicationDbContext db, ISiteDeviceAppService siteDeviceAppService)
            : base(db)
        {
            _siteDeviceAppService = siteDeviceAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _siteDeviceAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar Site
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SiteDeviceViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _siteDeviceAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Site por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<SiteDeviceViewModel> GetById(Guid id)
        {
            return  _siteDeviceAppService.GetById(id)    ;
        }
        /// <summary>
        /// Retorna lista de Sites  
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSiteDeviceByDeviceId{id}")]
        public IEnumerable<SiteDeviceViewModel> GetDevices()
        {
            return _siteDeviceAppService.GetAll();
        }

        [HttpGet("GetSiteDeviceBySiteId{id}")]
        public IEnumerable<SiteDeviceViewModel> GetSites()
        {
            return _siteDeviceAppService.GetAll();
        }


        /// <summary>
        /// Atualiza Site
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] SiteDeviceViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _siteDeviceAppService.Update(vm));
        }

        // get devices by site id
        [HttpGet("GetDevicesBySiteId")]
        public async Task<SiteDeviceViewModel> GetDevicesBySiteId(Guid id)
        {
            return  _siteDeviceAppService.GetById(id);
        }

        /// <summary>
        /// Retorna registros FK de Device
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/Sites")]
        public IEnumerable<Device> GetIdSites(Guid id)
        {
            return _siteDeviceAppService.GetDevicesBySiteDevice(id);
        }
        /// <summary>
        /// Retorna todos devices livres
        /// </summary>
        /// <returns></returns>
        [HttpGet("FreeDevices")]
        public IEnumerable<Device> GetFreeDevices()
        {
            return _siteDeviceAppService.GetAllFreeDevice();
        }
        /// <summary>
        /// Adicionar site x Devices
        /// </summary>
        /// <returns></returns>
        [HttpPost("SiteDevices")]
        public async Task<IActionResult> UpdateGamesByAccountId([FromBody] SiteDeviceMultipleVm vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_siteDeviceAppService.UpdateDeviceBySiteId(vm));
        }
    }
}