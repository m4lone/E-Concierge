using Microsoft.AspNetCore.Mvc;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.presentation.WebApp.Models;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class AccountGameController : BaseController<AccountGameViewModel>
    {
        private readonly IAccountGameAppService _accountGameAppService;
        private readonly IAccountAppService _accountAppService;
        private readonly IGameAppService _gameAppService;

        public AccountGameController(
            IAccountGameAppService accountGameAppService,
            IAccountAppService accountAppService,
            IGameAppService gameAppService,
            ApplicationDbContext db) : base(db)
        {
            _accountGameAppService = accountGameAppService;
            _accountAppService = accountAppService;
            _gameAppService = gameAppService;
        }

        public override IEnumerable<AccountGameViewModel> GetRows()
        {
            return _accountGameAppService.GetAll();
        }

        public override AccountGameViewModel GetRow(Guid id)
        {
            return _accountGameAppService.GetById(id);
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

        /******* consulta btn *game* *******/
        [HttpGet]
        public IActionResult GameByPartner(Guid id)
        {
            return View(GetGamesByAccountId(id));
        }

        public PartnerGamesViewModel GetGamesByAccountId(Guid id)
        {
            var vm = new PartnerGamesViewModel();
            if (Guid.Empty != id)
            {
                vm.AccountName = _accountAppService.GetById(id).Name;
                vm.Games = _accountGameAppService.GetGamesByAccountId(id).ToList();
            }
            return vm;
        }

        [HttpPost]
        public IActionResult Upsert(AccountGameViewModel vm)
        {
            return ViewDefault("Index", vm, _accountGameAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Accounts = _accountGameAppService.GetAccountsDrop().ToList();
            ViewBag.Games = _accountGameAppService.GetGamesDrop().ToList();
        }
    }
}
