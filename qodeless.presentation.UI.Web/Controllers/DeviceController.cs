using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.domain.Enums;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class DeviceController : BaseController<DeviceViewModel>
    {
        private readonly IDeviceAppService _deviceAppService;

        public DeviceController(IDeviceAppService deviceAppService, ApplicationDbContext db) : base(db)
        {
            _deviceAppService = deviceAppService;
        }

        public override IEnumerable<DeviceViewModel> GetRows()
        {
            return _deviceAppService.GetAllIndex();
        }

        public override DeviceViewModel GetRow(Guid id)
        {
            return _deviceAppService.GetById(id);
        }

        [HttpGet]
        public IActionResult Index(Guid Id)
        {
            if(Id != Guid.Empty)
            {
                return View(GetDevicesBySiteId(Id));
            }
            return View(GetRows());
        }

        [HttpGet]
        public IActionResult Upsert(Guid id)
        {
            return ViewForm(id);
        }

        [HttpPost]
        public IActionResult Upsert(DeviceViewModel vm)
        {
            return ViewDefault("Index", vm, _deviceAppService.Upsert(vm));
        }

        /****** pesquisa de device por partner (site) ******/
        public IEnumerable<DeviceViewModel> GetDevicesBySiteId(Guid siteId)
        {
            return _deviceAppService.GetDevicesBySiteId(siteId);
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }
        
        public async void LoadAsync()
        {
            ViewBag.Accounts = ( _deviceAppService.GetDevices()).ToList().Select(c => new SelectListItem(c.Code, c.Id.ToString()));
            
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem("Ativo", Convert.ToString((int) EAccountStatus.Actived)),
                new SelectListItem("Bloqueado", Convert.ToString((int) EAccountStatus.Blocked)),
                new SelectListItem("Inativo", Convert.ToString((int) EAccountStatus.Inatived))
            };
        }

        public IEnumerable<DeviceViewModel> GetDeviceBySiteId(Guid deviceId)
        {
            return _deviceAppService.GetDevicesBySiteId(deviceId);
        }
    }
}
