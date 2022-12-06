using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class SessionDeviceController : BaseController<SessionDeviceViewModel>
    {
        private readonly ISessionDeviceAppService _SessionDeviceAppService;
        private readonly ISiteAppService _siteAppService;
        private readonly IDeviceAppService _deviceAppService;
        private readonly IGameAppService _gameAppService;

        public SessionDeviceController(
            ISessionDeviceAppService SessionDeviceAppService,
             ISiteAppService siteAppService,
             IDeviceAppService deviceAppService,
             IGameAppService gameAppService,
             ApplicationDbContext db) : base(db)
        {
            _SessionDeviceAppService = SessionDeviceAppService;
            _siteAppService = siteAppService;
            _gameAppService = gameAppService;
            _deviceAppService = deviceAppService;
        }

        public override IEnumerable<SessionDeviceViewModel> GetRows()
        {
            return _SessionDeviceAppService.GetAll();
        }

        public override SessionDeviceViewModel GetRow(Guid id)
        {
            return _SessionDeviceAppService.GetById(id);
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
        public IActionResult Upsert(SessionDeviceViewModel vm)
        {
            return ViewDefault("Index",vm,_SessionDeviceAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Devices = _SessionDeviceAppService.GetDevicesDrop().ToList();
        }
    }
}
