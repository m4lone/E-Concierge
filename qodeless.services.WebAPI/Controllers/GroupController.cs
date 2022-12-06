using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    ///  Group
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ApiController
    {
        private readonly IGroupAppService _groupAppService;

        /// <summary>
        ///  Group
        /// </summary>
        /// <param name="db"></param>
        public GroupController(ApplicationDbContext db, IGroupAppService groupAppService)
            : base(db)
        {
            _groupAppService = groupAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _groupAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar Group
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.REGISTER_USER)]
        public async Task<IActionResult> Add([FromBody] GroupViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _groupAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Group por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GroupViewModel> GetById(Guid id)
        {
            return  _groupAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GroupViewModel>> GetGroups()
        {
            return  _groupAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Group
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GroupViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _groupAppService.Update(vm));
        }
    }
}