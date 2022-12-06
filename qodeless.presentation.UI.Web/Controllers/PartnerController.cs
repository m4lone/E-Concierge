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
    public class PartnerController : BaseController<AccountViewModel>
    {
        private readonly IAccountAppService _accountAppService;

        public PartnerController(IAccountAppService accountAppService, ApplicationDbContext db) : base(db)
        {
            _accountAppService = accountAppService;
        }

        public override IEnumerable<AccountViewModel> GetRows()
        {
            return _accountAppService.GetAllIndex();
        }

        public override AccountViewModel GetRow(Guid id)
        {
            return _accountAppService.GetById(id);
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
        public IActionResult Upsert(AccountViewModel vm)
        {
            return ViewDefault("Index", vm, _accountAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Accounts = _accountAppService.GetAccounts().ToList();

            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem("Ativo",Convert.ToString((int)EAccountStatus.Actived)),
                new SelectListItem("Bloqueado",Convert.ToString((int)EAccountStatus.Blocked)),
                new SelectListItem("Inativo",Convert.ToString((int)EAccountStatus.Inatived))
            };
        }
    }
}
