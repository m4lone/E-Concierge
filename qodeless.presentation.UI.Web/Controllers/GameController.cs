using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.Infra.CrossCutting.Identity.Repositories;
using qodeless.services.WebApp.Controllers;
using System;
using qodeless.domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.presentation.WebApp.Controllers
{
    public class GameController : BaseController<GameViewModel>
    {
        private readonly IGameAppService _gameAppService;

        public GameController(IGameAppService gameAppService, ApplicationDbContext db) : base(db)
        {
            _gameAppService = gameAppService;
        }

        public override IEnumerable<GameViewModel> GetRows()
        {
            return _gameAppService.GetAllIndex();
        }        

        [HttpGet]
        public IActionResult Index(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return View(GetGameByAccountId(Id));
            }
            return View(GetRows());
        }

        [HttpGet]
        public IActionResult Upsert(Guid id)
        {
            return ViewForm(id);
        }

        [HttpPost]
        public IActionResult Upsert(GameViewModel vm)
        {
            return ViewDefault("Index", vm, _gameAppService.Upsert(vm));
        }

        /****** pesquisa de game por partner (account) ******/
        public IEnumerable<GameViewModel> GetGameByAccountId(Guid accountId)
        {
            return _gameAppService.GetGamesByAccountId(accountId);
        }

        public override GameViewModel GetRow(Guid id)
        {
            return _gameAppService.GetById(id);
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }

        public async void LoadAsync()
        {
            ViewBag.Accounts = ( _gameAppService.GetGames()).ToList().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem("Ativo", Convert.ToString((int) EGameStatus.Actived)),
                new SelectListItem("Bloqueado", Convert.ToString((int) EGameStatus.Blocked)),
                new SelectListItem("Inativo", Convert.ToString((int) EGameStatus.Inatived))
            };
        }
    }
}
