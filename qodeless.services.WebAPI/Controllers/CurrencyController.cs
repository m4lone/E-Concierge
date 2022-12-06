using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using qodeless.domain.Entities.ACL;
using Microsoft.AspNetCore.Mvc;
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
using NLog;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  SuccessFee
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICurrencyAppService _currencyAppService;

        /// <summary>
        ///  SuccessFee
        /// </summary>
        /// <param name="db"></param>
        public CurrencyController(ApplicationDbContext db, ICurrencyAppService currencyAppService)
            : base(db)
        {
            _currencyAppService = currencyAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _currencyAppService.Remove(id));
        }
        /// <summary>
        /// Adicionar SuccessFee
        /// </summary>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] CurrencyViewModel vm)
        {
            var result = !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _currencyAppService.Add(vm));
            return result;
        }

        /// <summary>
        /// Retorna SuccessFee por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CurrencyViewModel> GetById(Guid id)
        {
            return  _currencyAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de SuccessFees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CurrencyViewModel>> GetCurrency()
        {
            return  _currencyAppService.GetAll();
        }

        /// <summary>
        /// Atualiza SuccessFee
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CurrencyViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _currencyAppService.Update(vm));
        }

        /// <summary>
        /// Retorna lista de Enums de ECurrencyCode
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrencyCodeEnums")]
        public async Task<IEnumerable<EnumCodeVm>> GetCurrencyCodeEnums()
        {
            return new List<EnumCodeVm>() {
                new EnumCodeVm("650",Convert.ToString((int)ECurrencyCode.EUR)),
                new EnumCodeVm("450",Convert.ToString((int)ECurrencyCode.USD)),

            };
        }
    }
}