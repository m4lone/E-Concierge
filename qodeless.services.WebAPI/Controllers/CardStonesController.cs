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
    ///  CardStones
    /// </summary>
    [Route("api/[controller]")]
    // [Authorize]
    public class CardStonesController : ApiController
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICardStonesAppService _cardStonesAppService;

        /// <summary>
        ///  CardStones
        /// </summary>
        /// <param name="db"></param>
        public CardStonesController(ApplicationDbContext db, ICardStonesAppService cardStonesAppService)
            : base(db)
        {
            _cardStonesAppService = cardStonesAppService;
        }

        /// <summary>
        /// Retorna lista de CardStones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CardStonesViewModel>> GetAllCardStones()
        {
            return _cardStonesAppService.GetAll();
        }

        /// <summary>
        /// Retorna CardStones por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CardStonesViewModel> GetCardStonesById(Guid id)
        {
            return _cardStonesAppService.GetById(id);
        }

        /// <summary>
        /// Adicionar CardStones
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] CardStonesAddViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_cardStonesAppService.GenerateCardNumber(vm));
        }

        /// <summary>
        /// Atualiza CardStones
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CardStonesViewModel vm)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(_cardStonesAppService.Update(vm));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(_cardStonesAppService.Remove(id));
        }

        /// <summary>
        /// Retorna lista de CardStoness por Game Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{siteId}/site")]
        public async Task<IEnumerable<CardStonesViewModel>> GetCardStonesByGameId(Guid siteId)
        {
            return _cardStonesAppService.GetAllBy(x => x.GameId.Equals(siteId));
        }
    }
}