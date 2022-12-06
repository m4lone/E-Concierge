using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Interfaces.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.presentation.WebApp.Controllers
{
    public class IncomeController : BaseController<IncomeViewModel>
    {
        private readonly IIncomeAppService _incomeAppService;

        public IncomeController(IIncomeAppService incomeAppService, ApplicationDbContext db) : base(db)
        {
            _incomeAppService = incomeAppService;
        }

        public override IEnumerable<IncomeViewModel> GetRows()
        {
            return _incomeAppService.GetAll();
        }

        public override IncomeViewModel GetRow(Guid id)
        {
            return _incomeAppService.GetById(id);
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
        public IActionResult Upsert(IncomeViewModel vm)
        {
            return ViewDefault("Index", vm, _incomeAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
             LoadAsync();
        }

        public async Task LoadAsync()
        {
            ViewBag.Accounts = _incomeAppService.GetAccountsDrop().ToList();
            ViewBag.IncomeTypes = _incomeAppService.GetIncomeTypesDrop().ToList();
            ViewBag.Sites = _incomeAppService.GetSitesDrop().ToList();
        } 
    }
}
