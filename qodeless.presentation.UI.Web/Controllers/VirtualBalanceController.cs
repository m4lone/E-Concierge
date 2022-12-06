using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;

namespace qodeless.presentation.WebApp.Controllers
{
    public class VirtualBalanceController : BaseController<VirtualBalanceViewModel>
    {

        private readonly IVirtualBalanceAppService _virtualBalanceAppService;
        private readonly IVirtualBalanceRepository _VirtualBalanceRepository;

        public VirtualBalanceController(IVirtualBalanceAppService virtualBalanceAppService, ApplicationDbContext db) : base(db)
        {
            _virtualBalanceAppService = virtualBalanceAppService;
        }

        public override IEnumerable<VirtualBalanceViewModel> GetRows()
        {
            return _virtualBalanceAppService.GetAll();
        }

        public override VirtualBalanceViewModel GetRow(Guid id)
        {
            return _virtualBalanceAppService.GetById(id);
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
        public IActionResult Upsert(VirtualBalanceViewModel vm)
        {
            return ViewDefault("Index",vm,_virtualBalanceAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem("Crédito",Convert.ToString((int)EBalanceType.Credit)),
                new SelectListItem("Débito",Convert.ToString((int)EBalanceType.Debit))
            };
        }        
    }
}
