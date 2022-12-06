using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Invite
    /// </summary>
    [Route("api/[controller]")]
    public class InviteController : ApiController
    {
        private readonly IInviteAppService _inviteAppService;

        /// <summary>
        ///  Invite
        /// </summary>
        /// <param name="db"></param>
        public InviteController(ApplicationDbContext db, IInviteAppService inviteAppService)
            : base(db)
        {
            _inviteAppService = inviteAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _inviteAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar Invite
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Guid siteid)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _inviteAppService.Add(siteid));
        }

        /// <summary>
        /// Retorna Invite por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<InviteViewModel> GetById(Guid id)
        {
            return  _inviteAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Invite por site ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBySiteId/{siteId}")]
        public async Task<IEnumerable<InviteViewModel>> GetBySiteId(Guid siteId)
        {
            return _inviteAppService.GetAllBy(x => x.SiteId.Equals(siteId));
        }

        /// <summary>
        /// Retorna lista de Invites
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<InviteViewModel>> GetInvites()
        {
            return  _inviteAppService.GetAll();
        }

    }
}