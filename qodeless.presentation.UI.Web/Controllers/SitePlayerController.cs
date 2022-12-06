using Microsoft.AspNetCore.Mvc;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.presentation.WebApp.Models;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class SitePlayerController : BaseController<SitePlayerViewModel>
    {
        private readonly ISitePlayerAppService _SitePlayerAppService;
        private readonly ISiteAppService _siteAppService;

        public SitePlayerController(
            ISitePlayerAppService SitePlayerAppService,
            ISiteAppService siteAppService,


             ApplicationDbContext db) : base(db)
        {
            _SitePlayerAppService = SitePlayerAppService;
            _siteAppService = siteAppService;
        }

        public override IEnumerable<SitePlayerViewModel> GetRows()
        {
            return _SitePlayerAppService.GetAll();
        }

        public override SitePlayerViewModel GetRow(Guid id)
        {
            return _SitePlayerAppService.GetById(id);
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
        public IActionResult Upsert(SitePlayerViewModel vm)
        {
            return ViewDefault("Index",vm,_SitePlayerAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        /******* consulta btn *Plays* *******/
        [HttpGet]
        public IActionResult PlaysBySites(Guid id)
        {
            return View(GetPlayersBySiteId(id));
        }

        public SitePlayersViewModel GetPlayersBySiteId(Guid id)
        {
            var vm = new SitePlayersViewModel();
            if (Guid.Empty != id)
            {
                vm.SiteName = _siteAppService.GetById(id).Name;
                vm.Plays = _SitePlayerAppService.GetPlayersBySiteId(id).ToList();
            }
            return vm;
        }

        public async void LoadAsync()
        {
            ViewBag.Sites = _SitePlayerAppService.GetSitesDrop().ToList();
            ViewBag.UserId = _SitePlayerAppService.GetUserDrop().ToList();
        }
    }
}
