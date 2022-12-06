using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.presentation.WebApp.Models;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class SiteDeviceController : BaseController<SiteDeviceViewModel>
    {
        private readonly ISiteDeviceAppService _siteDeviceAppService;
        private readonly IDeviceAppService _deviceAppService;
        private readonly ISiteAppService _siteAppService;

        public SiteDeviceController(
            ISiteDeviceAppService siteDeviceAppService,
            ISiteAppService siteAppService,
            IDeviceAppService deviceAppService,
             ApplicationDbContext db) : base(db)
        {
            _siteDeviceAppService = siteDeviceAppService;
            _siteAppService = siteAppService;
            _deviceAppService = deviceAppService;
        }

        public override IEnumerable<SiteDeviceViewModel> GetRows()
        {
            return _siteDeviceAppService.GetAll();
        }

        public override SiteDeviceViewModel GetRow(Guid id)
        {
            return _siteDeviceAppService.GetById(id);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetRows());
        }

        [HttpGet]
        public IActionResult Upsert(Guid id)
        {
            return ViewForm(id);
        }    

        [HttpPost]
        public IActionResult Upsert(SiteDeviceViewModel vm)
        {
            return ViewDefault("Index", vm, _siteDeviceAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Sites = _siteDeviceAppService.GetSitesDrop().ToList();
            ViewBag.Devices = _siteDeviceAppService.GetDevicesDrop().ToList();
        }

        /**** consulta devices by site id ******/
        [HttpGet]
        public IActionResult DeviceByPartner(Guid id)
        {
            return View(GetDevicesBySiteDeviceId(id));
        }
        public PartnerDevicesViewModel GetDevicesBySiteDeviceId(Guid id)
        {
            var vm = new PartnerDevicesViewModel();
            if (Guid.Empty != id)
            {
                //vm.SiteName = _siteAppService.GetAll().Where(x => x.AccountId == id).Select(x=>x.AccountName).FirstOrDefault();
                var siteid = _siteAppService.GetAll().Where(x => x.AccountId == id).Select(x => x.Id).FirstOrDefault();
                vm.Devices = _siteDeviceAppService.GetDevicesBySiteDevice(siteid).ToList();
            }

            return vm;
        }
    }
}
