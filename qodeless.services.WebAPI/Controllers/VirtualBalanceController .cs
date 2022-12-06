using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qodeless.domain.Entities.ACL;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.domain.Enums.Model;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using qodeless.domain.Enums;
using qodeless.domain.Interfaces.Repositories;
using qodeless.application.Extensions;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Account
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class VirtualBalanceController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IVirtualBalanceAppService _VirtualBalanceAppService;
        private readonly IVirtualBalanceRepository _VirtualBalanceRepository;
        


        /// <summary>
        ///  Account
        /// </summary>
        /// <param name="db"></param>
        public VirtualBalanceController(ApplicationDbContext db, IVirtualBalanceAppService VirtualBalanceAppService)
            : base(db)
        {
            _VirtualBalanceAppService = VirtualBalanceAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _VirtualBalanceAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VirtualBalanceViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _VirtualBalanceAppService.Add(vm));
        }

        /// <summary>
        /// Retorna Account por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<VirtualBalanceViewModel> GetById(Guid id)
        {
            return  _VirtualBalanceAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<VirtualBalanceViewModel>> GetAccounts()
        {
            return  _VirtualBalanceAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] VirtualBalanceViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _VirtualBalanceAppService.Update(vm));
        }

        [HttpGet("GetTotalBalances")]
        public VirtualBalanceViewModel GetTotalBalances()
        {
            var totalBalances = _VirtualBalanceAppService.GetTotalBalances();
            return totalBalances;
        }

        [HttpGet("GetResumeByCredit")]
        public async Task<IActionResult> GetResumeByCredit(Guid siteId, DateTime dtBegin, DateTime dtEnd)
        {
            var query = from a in Db.VirtualBalances
                        where a.Voucher.SiteID == siteId
                        && a.Type == EBalanceType.Credit
                        && a.CreatedAt.IsBetween(dtBegin, dtEnd)
                        select new VirtualBalanceResponse
                        {
                            Amount = a.Amount,
                        };

            double total = query.Sum(item => item.Amount);
            return Ok(total);

        }

        [HttpGet("GetResumeByDebit")]
        public async Task<IActionResult> GetResumeByDebit(Guid siteId, DateTime dtBegin, DateTime dtEnd)
        {
            var query = from a in Db.VirtualBalances
                        where a.Voucher.SiteID == siteId
                        && a.Type == EBalanceType.Debit
                        && a.CreatedAt.IsBetween(dtBegin,dtEnd)
                        select new VirtualBalanceResponse
                        {
                            Amount = a.Amount,
                        };

            double total = query.Sum(item => item.Amount);
            return Ok(total);

        }

        /// <summary>
        /// Checa por UserPlayerId se existe algum um registro no Virtual Balance com status Pendente
        /// O aplicativo fica verificando este endpoint a cada X segundos
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("PoolingForPendingBalancesByUserPlayerId/{id}")]
        public bool PoolingForPendingBalancesByUserPlayerId(Guid id)
        {
            return _VirtualBalanceAppService.PoolingForPendingBalancesByUserPlayerId(id);
        }

        /// <summary>
        /// endpoint criado para consumir créditos possuidos por um usúario em status pendente
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="siteId"></param>
        /// <returns>Valor de crédito do usuario atualizado</returns>
        [AllowAnonymous]
        [HttpGet("ConsumeOutstandingCredits/{userId}/{siteId}")]
        public IActionResult ConsumeOutstandingCredits(string userId,Guid siteId)
        {
            var result = _VirtualBalanceAppService.GetAndUpdateTotalPendingBalances(userId, siteId);

            return result == null ? BadRequest() : Ok(new { success = true , result = result});
        }

        /// <summary>
        /// Consulta inicial que retorna a soma do Amount por UserPlayerId com a condicao Paid + Pendind+ (Debit* -1) 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("PoolingSumAmountByUserPlayerId/{id}")]
        public double PoolingSumAmountByUserPlayerId(Guid id)
        {
            return _VirtualBalanceAppService.PoolingSumAmountByUserPlayerId(id);
        }
    }
}