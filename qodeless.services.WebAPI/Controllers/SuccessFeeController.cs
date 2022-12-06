using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Enums;
using qodeless.domain.Enums.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  SuccessFee
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class SuccessFeeController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISuccessFeeAppService _successFeeAppService;

        /// <summary>
        ///  SuccessFee
        /// </summary>
        /// <param name="db"></param>
        public SuccessFeeController(ApplicationDbContext db, ISuccessFeeAppService successFeeAppService)
            : base(db)
        {
            _successFeeAppService = successFeeAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _successFeeAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar SuccessFee
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SuccessFeeViewModel vm)
        {
            var result = !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _successFeeAppService.Add(vm));
            return result;
        }

        /// <summary>
        /// Retorna SuccessFee por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<SuccessFeeViewModel> GetById(Guid id)
        {
            return  _successFeeAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de SuccessFees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SuccessFeeViewModel>> GetSuccessFees()
        {
            return  _successFeeAppService.GetAll();
        }

        /// <summary>
        /// Atualiza SuccessFee
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] SuccessFeeViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _successFeeAppService.Update(vm));
        }

        /// <summary>
        /// Retorna lista de Enums de SuccessFeeType
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSuccessFeeTypeEnums")]
        public async Task<IEnumerable<EnumTypeVm>> GetSuccessFeeTypeEnums()
        {
            return new List<EnumTypeVm>() {
                new EnumTypeVm("Conta",Convert.ToString((int)EFeeType.ByAccount)),
                new EnumTypeVm("Parceria",Convert.ToString((int)EFeeType.ByParnetUser)),
                new EnumTypeVm("Casa",Convert.ToString((int)EFeeType.BySite)),
            };
        }

        /// <summary>
        /// Retorna registros FK de Account
        /// ComboBox = DropDown List
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAccountsCmb")]
        public async Task<IEnumerable<EnumTypeVm>> GetAccountsCmb()
        {
            return _successFeeAppService.GetAccounts().Select(c => new EnumTypeVm(c.Name,c.Id));
        }
        
        //Endpoints extras 

        [HttpGet("GetSuccessFeeByAccountId{id}")]
        public async Task<IEnumerable<SuccessFeeViewModel>> GetSuccessFeeByAccountId(Guid id)
        {  
            var success = _successFeeAppService.GetByAccountId(id);     
           return success;
        }

        [HttpGet("GetSuccessFeeBySiteId{id}")]
        public async Task<IEnumerable<SuccessFeeViewModel>> GetSuccessFeeBySiteId(Guid id)
        {
            var site = _successFeeAppService.GetBySiteId(id);
            return site;
        }

        [HttpGet("GetSuccessFeeByUserId{id}")]
        public async Task<IEnumerable<SuccessFeeViewModel>> GetSuccessFeeByUserId(string id)
        {
            var user = _successFeeAppService.GetByUserId(id);
            return user;
        }

        //extra endpoints para somar Rate

        [HttpGet("GetTotalFeeByAccountId{id}")]
        public Double GetTotalFeeByAccountId(Guid id)
        {
            var sum = Db.SuccessFees.Where(u => u.AccountId == id).Sum(c => c.Rate);   

            return sum;
        }

        [HttpGet("GetTotalFeeBySiteId{id}")]
        public Double GetTotalFeeBySiteId(Guid id)
        {
            var sum = Db.SuccessFees.Where(u => u.SiteId == id).Sum(c => c.Rate);

            return sum;
        }

        [HttpGet("GetTotalFeeByUserId{id}")]
        public Double GetTotalFeeByUserId(string id)
        {
            var sum = Db.SuccessFees.Where(u => u.UserId == id).Sum(c => c.Rate);

            return sum;
        }
    }
}