using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using qodeless.application;
using qodeless.application.Extensions;
using qodeless.application.ViewModels;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Vouscher
    /// </summary>
    [Route("api/[controller]")]
   // [Authorize]
    public class VouscherController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IVouscherAppService _vouscherAppService;

        /// <summary>
        ///  Vouscher
        /// </summary>
        /// <param name="db"></param>
        public VouscherController(ApplicationDbContext db, IVouscherAppService vouscherAppService)
            : base(db)
        {
            _vouscherAppService = vouscherAppService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse( _vouscherAppService.Remove(id));
        }

        /// <summary>
        /// Retorna Vouscher por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<VouscherViewModel> GetById(Guid id)
        {
            return  _vouscherAppService.GetById(id);
        }

        /// <summary>
        /// Retorna lista de Vouschers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<VouscherViewModel>> GetVouschers()
        {
            return  _vouscherAppService.GetAll();
        }
        /// <summary>
        /// Retorna lista de Vouchers por site id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{siteId}/site")]
        public async Task<IEnumerable<VouscherViewModel>> GetVoucherBySiteId(Guid siteId)
        {
            return _vouscherAppService.GetAllBySiteId(x => x.SiteID.Equals(siteId));
        }
        /// <summary>
        /// Atualiza Vouscher
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] VouscherViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse( _vouscherAppService.Update(vm));
        }
        /// <summary>
        /// Desativa Voucher
        /// </summary>
        /// <returns></returns>
        [HttpPut("Disable/{id}")]
        public async Task<IActionResult> Disable(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_vouscherAppService.DisableVoucher(id));
        }
        /// <summary>
        /// Adicionar Vouscher
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] VouscherViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_vouscherAppService.GenerateVoucherCode(vm));
        }

        [HttpGet("GenerateRandomVoucherCode")]
        public string GenerateRandomVoucherCode()
        {
            var number = "";
            var random = new Random();

            string letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder resultStringBuilder = new StringBuilder();

            for (int i = 0; i < 2; i++)
            {
                resultStringBuilder.Append(letter[random.Next(letter.Length)]);
            }

            for (int i = 0; i < 4; i++)
            {
                number += new Random().Next(0, 9).ToString();
            }

            string voucherCode = "RG" + number + resultStringBuilder;

            return voucherCode;

        }
        
        [HttpPost("VerifyPayVoucherCode")]
        public IActionResult VerifyPayVoucherCode(List<string> voucherCode, string userPlayId, EBalanceType type, string description, string userOperationId)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_vouscherAppService.VerifyPayVoucherCode(voucherCode, userPlayId, type, description, userOperationId));
        }

        [HttpGet("GetCardVoucher")]
        public async Task<IActionResult> GetCardVoucher(Guid siteId)
        {
            var query = (from v in Db.Vouchers
                        where v.SiteID == siteId
                        select new VirtualBalanceResponse
                        {
                            Amount = v.Amount,
                        }).ToList();

            double total = query.Sum(item => item.Amount);
            return Ok(total);

        }
    }
}