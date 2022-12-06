using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class SiteController : BaseController<SiteViewModel>
    {
        private readonly ISiteAppService _siteAppService;
        private readonly IAccountAppService _accountAppService;

        public SiteController(
            ISiteAppService siteAppService,
             IAccountAppService accountAppService,
             ApplicationDbContext db) : base(db)
        {
            _siteAppService = siteAppService;
            _accountAppService = accountAppService;
        }

        public override IEnumerable<SiteViewModel> GetRows()
        {
            return _siteAppService.GetAll();
        }

        public override SiteViewModel GetRow(Guid id)
        {
            return _siteAppService.GetById(id);
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
        public IActionResult Upsert(SiteViewModel vm)
        {
            return ViewDefault("Index",vm,_siteAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Accounts = _siteAppService.GetAccountsDropDown().ToList();

            ViewBag.SiteTypes = new List<SelectListItem>()
            {
                new SelectListItem("Cassino", Convert.ToString((int) ESiteType.Casino)),
                new SelectListItem("Padrão", Convert.ToString((int) ESiteType.Default)),
                new SelectListItem("Nuvem", Convert.ToString((int)  ESiteType.Cloud)),
                new SelectListItem("Outros", Convert.ToString((int)  ESiteType.Other)),
                new SelectListItem("LanHouse", Convert.ToString((int)  ESiteType.LanHouse))
            };
        }
    }
}
