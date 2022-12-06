using Microsoft.AspNetCore.Mvc;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class DeviceGameController : BaseController<DeviceGameViewModel>
    {
        private readonly IDeviceGameAppService _deviceGameAppService;

        public DeviceGameController(IDeviceGameAppService deviceGameAppService, ApplicationDbContext db) : base(db)
        {
            _deviceGameAppService = deviceGameAppService;
        }

        public override IEnumerable<DeviceGameViewModel> GetRows()
        {
            return _deviceGameAppService.GetAll();
        }

        public override DeviceGameViewModel GetRow(Guid id)
        {
            return _deviceGameAppService.GetById(id);
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
        public IActionResult Upsert(DeviceGameViewModel vm)
        {
            return ViewDefault("Index", vm, _deviceGameAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Devices = _deviceGameAppService.GetDevicesDrop().ToList();
            ViewBag.Games = _deviceGameAppService.GetGamesDrop().ToList();
        }
    }
}
