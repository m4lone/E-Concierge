using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;

namespace qodeless.presentation.WebApp.Controllers
{
    public class CurrencyController : BaseController<CurrencyViewModel>
    {
        private readonly ICurrencyAppService _currencyAppService;

        public CurrencyController(ICurrencyAppService currencyAppService, ApplicationDbContext db) : base(db)
        {
            _currencyAppService = currencyAppService;
        }

        public override IEnumerable<CurrencyViewModel> GetRows()
        {
            return _currencyAppService.GetAll();
        }

        public override CurrencyViewModel GetRow(Guid id)
        {
            return _currencyAppService.GetById(id);
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
        public IActionResult Upsert(CurrencyViewModel vm)
        {
            return ViewDefault("Index",vm,_currencyAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem("Real",Convert.ToString((int)ECurrencyCode.BRL)),
                new SelectListItem("Dolar",Convert.ToString((int)ECurrencyCode.USD)),
                new SelectListItem("Euro",Convert.ToString((int)ECurrencyCode.EUR))

            };
        } 
    }
}
