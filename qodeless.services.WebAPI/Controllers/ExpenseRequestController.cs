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
    ///  ExpenseRequest
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ExpenseRequestController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IExpenseRequestAppService _expenseRequestAppService;

        /// <summary>
        ///  Expense
        /// </summary>
        /// <param name="db"></param>
        public ExpenseRequestController(ApplicationDbContext db, IExpenseRequestAppService expenseRequestAppService)
            : base(db)
        {
            _expenseRequestAppService = expenseRequestAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _expenseRequestAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar ExpenseRequest
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ExpenseRequestViewModel vm)
        {
            var result = !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _expenseRequestAppService.Add(vm));
            return result;
        }

        /// <summary>
        /// Retorna Expense por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ExpenseRequestViewModel> GetById(Guid id)
        {
            return  _expenseRequestAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Expenses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ExpenseRequestViewModel>> GetExpenseRequests()
        {
            return  _expenseRequestAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Expense
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ExpenseRequestViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _expenseRequestAppService.Update(vm));
        }

        /// <summary>
        /// Retorna lista de Enums de ExpenseType
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetExpenseRequestStatusEnums")]
        public async Task<IEnumerable<EnumStatusVm>> GetExpenseRequestStatusEnums()
        {
            var request = new EExpenseRequest();
            return new List<EnumStatusVm>() {
                new EnumStatusVm("Pendente",Convert.ToString((int)EExpenseRequest.Denied)),
                new EnumStatusVm("Aprovado",Convert.ToString((int)EExpenseRequest.Approved)),
            };       
        }
    }
}