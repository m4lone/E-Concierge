using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    ///  Expense
    /// </summary>
    [Route("api/[controller]")]
    public class ExpenseController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IExpenseRequestAppService _expenseRequestAppService;
        private readonly IExpenseAppService _expenseAppService;

        /// <summary>
        ///  Expense
        /// </summary>
        /// <param name="db"></param>
        public ExpenseController(ApplicationDbContext db, IExpenseAppService expenseAppService)
            : base(db)
        {
            _expenseAppService = expenseAppService;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return CustomResponse(_expenseAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Expense
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] ExpenseViewModel vm)
        {
            var result = !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_expenseAppService.Add(vm));
            return result;
        }

        /// <summary>
        /// Retorna Expense por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ExpenseViewModel GetById(Guid id)
        {
            return _expenseAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Expenses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ExpenseViewModel> GetExpenses()
        {
            return _expenseAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Expense
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] ExpenseViewModel vm)
        {

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_expenseAppService.Update(vm));
        }


        /// <summary>
        /// Retorna lista de Enums de ExpenseStatus
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetExpenseStatusEnums")]
        public IEnumerable<EnumStatusVm> GetExpenseStatusEnums()
        {
            return new List<EnumStatusVm>() {
                new EnumStatusVm("Pendente",Convert.ToString((int)EExpenseStatus.Pending)),
                new EnumStatusVm("Aprovado",Convert.ToString((int)EExpenseStatus.Approved)),
                new EnumStatusVm("Pago",Convert.ToString((int)EExpenseStatus.Paid)),
                new EnumStatusVm("Cancelado",Convert.ToString((int)EExpenseStatus.Cancelled)),
            };
        }

        [HttpPost("ApproveExpense{id}")]
        public IEnumerable<EnumStatusVm> ApproveExpense(EAccountType eAccountType)
        {
            return new List<EnumStatusVm>() {
                new EnumStatusVm("Aprovado",Convert.ToString((int)EExpenseStatus.Approved)),
                new EnumStatusVm("Aprovado",Convert.ToString((int)EExpenseRequest.Approved)),
            };

        }

        [HttpPost("AddExpenseSiteAdmin")]
        public IActionResult AddExpenseSiteAdmin([FromBody] ExpenseViewModel vm, Guid userId)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_expenseAppService.AddExpenseSiteAdmin(vm, userId));
        }

        [HttpPost("AddExpensePartner")]
        public IActionResult AddExpensePartner([FromBody] ExpenseViewModel vm, Guid userId)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_expenseAppService.AddExpensePartner(vm, userId));
        }

        /// <summary>
        /// Retorna registros FK de Account
        /// ComboBox = DropDown List
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSitesCmb")]
        public IEnumerable<EnumTypeVm> GetSitesCmb()
        {
            return _expenseAppService.GetSites().Select(c => new EnumTypeVm(c.Name, c.Id));
        }

        /// <summary>
        /// Retorna registros FK de Expense
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/Sites")]
        public IEnumerable<ExpenseViewModel> GetIdSites(Guid id)
        {
            return _expenseAppService.GetExpense(id);
        }

        /// <summary>
        /// Retorna lista de Expenses
        /// </summary>
        /// <returns></returns>
        [HttpGet("ExpenseBySite")]
        public IEnumerable<ExpenseViewModel> GetExpensesBySite()
        {
            var result = new List<ExpenseViewModel>();
            var list = _expenseAppService.GetAll();

            foreach (var expenses in list)
            {
                var request = _expenseRequestAppService.GetAll().Where(_ => _.ExpenseId == expenses.Id).FirstOrDefault();
                result.Add(new ExpenseViewModel
                {
                    AccountId = expenses.AccountId,
                    AccountName = expenses.AccountName,
                    Amount = expenses.Amount,
                    Description = expenses.Description,
                    DueDate = request.DueDate,
                    Id = expenses.Id,
                    SiteId = expenses.SiteId,
                    SiteName = expenses.SiteName,
                    Type = expenses.Type
                });
            }

            if (result.Count() < 1)
            {
                return (IEnumerable<ExpenseViewModel>)NotFound("NOT FOUND EXPENSES FOR THIS USER");
            }
            return result.OrderBy(_ => _.DueDate);
        }
    }
}