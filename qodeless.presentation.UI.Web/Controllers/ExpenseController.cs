using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ExpenseController : BaseController<ExpenseViewModel>
    {
        private readonly IExpenseAppService _expenseAppService;

        public ExpenseController(IExpenseAppService expenseAppService, ApplicationDbContext db) : base(db)
        {
            _expenseAppService = expenseAppService;
        }

        public override IEnumerable<ExpenseViewModel> GetRows()
        {
            return _expenseAppService.GetAll();
        }

        public override ExpenseViewModel GetRow(Guid id)
        {
            return _expenseAppService.GetById(id);
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
        public IActionResult Upsert(ExpenseViewModel vm)
        {
            return ViewDefault("Index",vm,_expenseAppService.Upsert(vm));
        } 

        public override void LoadViewBags()
        {
            LoadAsync();
        }
        
        public async void LoadAsync()
        {
            ViewBag.Sites = ( _expenseAppService.GetSites()).ToList().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
        }
    }
}
