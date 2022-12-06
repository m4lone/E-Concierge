using Microsoft.AspNetCore.Mvc;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.services.WebApp.Controllers;
using System;
using System.Collections.Generic;

namespace qodeless.presentation.WebApp.Controllers
{
    public class GroupController : BaseController<GroupViewModel>
    {
        private readonly IGroupAppService _groupAppService;

        public GroupController(IGroupAppService groupAppService, ApplicationDbContext db) : base(db)
        {
            _groupAppService = groupAppService;
        }

        public override IEnumerable<GroupViewModel> GetRows()
        {
            return _groupAppService.GetAll();
        }

        public override GroupViewModel GetRow(Guid id)
        {
            return _groupAppService.GetById(id);
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
        public IActionResult Upsert(GroupViewModel vm)
        {
            return ViewDefault("Index",vm,_groupAppService.Upsert(vm));
        }

        public override void LoadViewBags()
        {            
        } 
    }
}
