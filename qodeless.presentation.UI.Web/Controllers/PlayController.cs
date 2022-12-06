using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class PlayController : BaseController<PlayViewModel>
    {
        private readonly IPlayAppService _playAppService;

        public PlayController(IPlayAppService playAppService, ApplicationDbContext db) : base(db)
        {
            _playAppService = playAppService;
        }

        public override IEnumerable<PlayViewModel> GetRows()
        {
            return _playAppService.GetAll();
        }

        public override PlayViewModel GetRow(Guid id)
        {
            return _playAppService.GetById(id);
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
        public IActionResult Upsert(PlayViewModel vm)
        {
            return ViewDefault("Index",vm,_playAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }
        
        public async void LoadAsync()
        {
            ViewBag.SessionDevices = _playAppService.GetUserSessionDeviceIdDrop().ToList();
            ViewBag.Devices = _playAppService.GetUserDeviceCodeDrop().ToList();
            ViewBag.Games = _playAppService.GetGameNameDrop().ToList();
            ViewBag.Accounts = _playAppService.GetAccountNameDrop().ToList();
            ViewBag.Users = GetUsers();
        }
    }
}
