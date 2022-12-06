using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.Interfaces;
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
    public class IncomeController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IIncomeAppService _incomeAppService;

        /// <summary>
        ///  SuccessFee
        /// </summary>
        /// <param name="db"></param>
        public IncomeController(ApplicationDbContext db, IIncomeAppService incomeAppService)
            : base(db)
        {
            _incomeAppService = incomeAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _incomeAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar SuccessFee
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IncomeViewModel vm)
        {
            var result = !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _incomeAppService.Add(vm));
            return result;
        }

        /// <summary>
        /// Retorna SuccessFee por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IncomeViewModel> GetById(Guid id)
        {
            return  _incomeAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de SuccessFees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<IncomeViewModel>> GetIncomes()
        {
            return  _incomeAppService.GetAll();
        }

        /// <summary>
        /// Atualiza SuccessFee
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] IncomeViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _incomeAppService.Update(vm));
        }

        /// <summary>
        /// Retorna lista de Enums de EbalanceType
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetIncomeTypeEnums")]
        public async Task<IEnumerable<EnumTypeVm>> GetIncomeTypeEnums()
        {
            return new List<EnumTypeVm>() {
                new EnumTypeVm("789",Convert.ToString((int)EIncomeType.SuccessFee)),
                new EnumTypeVm("654",Convert.ToString((int)EIncomeType.Play)),
            };
        }

        /// <summary>
        /// Retorna lista de Enums de EbalanceType
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddIncome{id}")]
        public async Task<IEnumerable<EnumTypeVm>> AddIncome()
        {
            return new List<EnumTypeVm>() {
                new EnumTypeVm("789",Convert.ToString((int)EIncomeType.SuccessFee)),
                new EnumTypeVm("654",Convert.ToString((int)EIncomeType.Play)),
            };
        }

        [HttpGet("GetIncomeStatusEnums")]
        public async Task<IEnumerable<EnumStatusVm>> GetIncomeStatusEnums()
        {
            return new List<EnumStatusVm>() {
                new EnumStatusVm("328",Convert.ToString((int)EIncomeStatus.Approved)),
                new EnumStatusVm("765",Convert.ToString((int)EIncomeStatus.Paid)),
            };
        }

        [HttpPost("AddIncomePartner")]
        //[Authorize(Roles = "PARTNER")]
        //[Authorize(Roles = "SITE_ADMIN")]
        [AllowAnonymous]
        public IActionResult AddIncomePartner([FromBody] IncomeViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_incomeAppService.AddIncomePartner(vm));
        }

        [HttpPost("CheckedIncome")]        
        public IActionResult CheckedIncome([FromBody] IncomeViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_incomeAppService.CheckedIncome(vm));
        }

        /// <summary>
        /// Retorna lista de Enums de EbalanceType
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddIncomeStatus{id}")]
        public async Task<IEnumerable<EnumStatusVm>> AddIncomeStatus()
        {
            return new List<EnumStatusVm>() {
                new EnumStatusVm("328",Convert.ToString((int)EIncomeStatus.Approved)),
                new EnumStatusVm("765",Convert.ToString((int)EIncomeStatus.Paid)),
            };
        }

        /// <summary>
        /// Retorna lista de Income com siteId
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/Sites")]
        public IEnumerable<IncomeViewModel> GetIdIncome(Guid id)
        {
            return _incomeAppService.GetIncome(id);
        }
    }
}