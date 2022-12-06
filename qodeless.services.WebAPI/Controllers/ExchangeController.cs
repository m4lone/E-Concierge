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
    ///  Exchange
    /// </summary>
    [Route("api/[controller]")]
    public class ExchangeController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IExchangeAppService _exchangeAppService;

        /// <summary>
        ///  Exchange
        /// </summary>
        /// <param name="db"></param>
        public ExchangeController(ApplicationDbContext db, IExchangeAppService exchangeAppService)
            : base(db)
        {
            _exchangeAppService = exchangeAppService;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return CustomResponse(_exchangeAppService.Remove(id));
        }

        /// <summary>
        /// Adicionar Exchange
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] ExchangeViewModel vm)
        {
            var result = !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_exchangeAppService.Add(vm));
            return result;
        }

        /// <summary>
        /// Retorna Exchange por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ExchangeViewModel GetById(Guid id)
        {
            return _exchangeAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Exchanges
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ExchangeViewModel> GetExchanges()
        {
            return _exchangeAppService.GetAll();
        }

        /// <summary>
        /// Atualiza Exchange
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] ExchangeViewModel vm)
        {

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_exchangeAppService.Update(vm));
        }

        [HttpPost("ApproveExchange")]
        public IActionResult ApproveExchange(Guid exchangeId)
        {
            var exist = Db.Exchanges.Where(_ => _.Id == exchangeId).FirstOrDefault();

            exist.ExchangeStatus = EExchangeStatus.Approved;
            var save = Db.Update(exist);
            var result = Db.SaveChanges();
            
            return Ok(result);
        }

    }
}