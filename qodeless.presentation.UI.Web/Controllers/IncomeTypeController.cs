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
    public class IncomeTypeController : BaseController<IncomeTypeViewModel>
    {
        private readonly IIncomeTypeAppService _incomeTypeAppService;

        public IncomeTypeController(IIncomeTypeAppService incomeTypeAppService, ApplicationDbContext db) : base(db)
        {
            _incomeTypeAppService = incomeTypeAppService;
        }

        public override IEnumerable<IncomeTypeViewModel> GetRows()
        {
            return _incomeTypeAppService.GetAll();
        }

        public override IncomeTypeViewModel GetRow(Guid id)
        {
            return _incomeTypeAppService.GetById(id);
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
        public IActionResult Upsert(IncomeTypeViewModel vm)
        {
            return ViewDefault("Index", vm, _incomeTypeAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        { 
        }

        public async Task LoadAsync()
        {           
        } 
    }
}
