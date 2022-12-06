using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qodeless.application;
using qodeless.application.ViewModels;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebAPI.Controllers
{
    /// <summary>
    ///  Order
    /// </summary>
    [Route("api/[controller]")]
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderService;
        public OrderController(
            ApplicationDbContext db,
            IOrderServices orderService
            ) : base(db)
        {
            _orderService = orderService; 
        }

        #region CRUD
        [HttpGet("PixId")]
        public async Task<IActionResult> Get(string pixId)
        {
            var result = await Task.Run(() => _orderService.GetByPixId(pixId));

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Task.Run(() =>_orderService.GetById(id));
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await Task.Run(() =>_orderService.GetAll().OrderByDescending(y => y.CreatedAt).ToList());
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Task.Run(() => _orderService.Remove(id));
            if (result)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(OrderViewModel vm)
        {
            var result = await Task.Run(() => _orderService.Add(vm));
            if(result)
                   return Ok();
            return BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(OrderViewModel vm)
        {
            var result = await Task.Run(() => _orderService.Update(vm));
            if (result)
                return Ok();
            return BadRequest();
        }
        #endregion

        //PIX ORDER DISABLE - ADRIAN
        #region PIX CONTROLLERS
        //[HttpGet("QRCODE")]
        //public async Task<IActionResult> QrCode(string pixId)
        //{
        //    var user = GetLoggedUser();
        //    return Ok(await Task.Run(() => _orderService.GenerateQrCode(user.PixUserName, user.PixPassword, pixId, user.PixApiKey, user.PixApiSecret)));
        //}

        [HttpPost("PixCode")]
        public async Task<ResponsePixViewModel> PixCode([FromBody] PixCodeViewModel vm)
        {
            //valores para teste
            //var user = GetLoggedUser(); await Task.Run(() => _orderService.GetPixCode(1, 0, 1, "45204580000177", "zqcd2ZvTIaK3km6X", "c4f41668-4b55-4b3b-8df5-137a5996573a", "35606862-050f-4ca4-aa06-a981583559a2", "ca899f68-4328-4b45-8073-73637f371301", Guid.Parse("6fcf2faf-46d8-452d-b0ea-ad470396be75"),null));
            //await Task.Run(() => _orderService.GetPixCode(1, 0, 1, "45204580000177", "zqcd2ZvTIaK3km6X", "99aa0aca-e80f-40a2-9d82-dad634925ad4", "35606862-050f-4ca4-aa06-a981583559a2", "ca899f68-4328-4b45-8073-73637f371301", Guid.Parse("6fcf2faf-46d8-452d-b0ea-ad470396be75"), Guid.Parse("65728607-67ba-44fb-9548-73ceb6c8c3f6")));
            //return await Task.Run(() => _orderService.GetPixCode(1, 0, 1, "45204580000177", "zqcd2ZvTIaK3km6X", "4a8726a4-a28a-4977-a9a0-0ff6705decec", "35606862-050f-4ca4-aa06-a981583559a2", "ca899f68-4328-4b45-8073-73637f371301", null, null));
            return await Task.Run(() => _orderService.GetPixCode(vm.Fee, vm.Value, vm.ClientId, vm.UserId, vm.ClientSecret, vm.SiteId, vm.AccountId));
        }

        //[HttpPost("VariousPixCode")]
        //public async Task<List<ResponsePixViewModel>> VariousPixCode([FromBody] VariousPixCodeViewModel vm)
        //{
        //    var user = GetLoggedUser();
        //    return await Task.Run(() => _orderService.GetVariousPixCode(vm.Amount, user.PixUserName, user.PixPassword, vm.Quantity, user.UserId, user.PixApiKey, user.PixApiSecret)
        //    .OrderByDescending(y => y.CreatedAt).ToList());
        //}

        [HttpGet("PixAuth")]
        public async Task<ResponseAuthViewModel> PixAuth()
        {
            return _orderService.GetPixAuth("7", "43QESGXSHDi1eAY5L1cd4eOaT8eIH8BpG39UgydM");
        }

        //[HttpGet("GetPixStatus")]
        //public async Task<ResponsePixViewModel> GetPixStatus(string pixId)
        //{
        //    var user = GetLoggedUser();
        //    return await Task.Run(() => _orderService.GetPixStatus(user.PixUserName, user.PixPassword, pixId, user.PixApiKey, user.PixApiSecret));
        //}

        //[HttpGet("CheckPixIsPaid")]
        //public async Task<bool> CheckPixIsPaid(string pixId)
        //{
        //    var user = GetLoggedUser();
        //    return await Task.Run(() => _orderService.CheckPixIsPaid(user.PixUserName, user.PixPassword, pixId, user.PixApiKey, user.PixApiSecret));
        //}
        #endregion
    }
}
