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
    public class ExpenseRequestController : BaseController<ExpenseRequestViewModel>
    {
        private readonly IExpenseRequestAppService _expenseRequestAppService;

        public ExpenseRequestController(IExpenseRequestAppService expenseRequestAppService, ApplicationDbContext db) : base(db)
        {
            _expenseRequestAppService = expenseRequestAppService;
        }

        public override IEnumerable<ExpenseRequestViewModel> GetRows()
        {
            return _expenseRequestAppService.GetAll();
        }

        public override ExpenseRequestViewModel GetRow(Guid id)
        {
            return _expenseRequestAppService.GetById(id);
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
        public IActionResult Upsert(ExpenseRequestViewModel vm)
        {
            return ViewDefault("Index",vm, _expenseRequestAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }
        
        public async void LoadAsync()
        {
            ViewBag.Expenses = _expenseRequestAppService.GetExpensesDrop().ToList();
        }
    }
}
