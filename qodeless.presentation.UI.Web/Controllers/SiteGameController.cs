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
    public class SiteGameController : BaseController<SiteGameViewModel>
    {
        private readonly ISiteGameAppService _siteGameAppService;    

        public SiteGameController(
            ISiteGameAppService siteGameAppService,
             
             ApplicationDbContext db) : base(db)
        {
            _siteGameAppService = siteGameAppService;           
        }

        public override IEnumerable<SiteGameViewModel> GetRows()
        {
            return _siteGameAppService.GetAll();
        }

        public override SiteGameViewModel GetRow(Guid id)
        {
            return _siteGameAppService.GetById(id);
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
        public IActionResult Upsert(SiteGameViewModel vm)
        {
            return ViewDefault("Index",vm,_siteGameAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Sites = _siteGameAppService.GetSitesDrop().ToList();
            ViewBag.Games = _siteGameAppService.GetGamesDrop().ToList();
        }
    }
}
