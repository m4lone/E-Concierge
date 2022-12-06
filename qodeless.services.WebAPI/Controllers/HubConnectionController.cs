using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qodeless.services.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qodeless.services.WebApi.Manager;

namespace qodeless.services.WebApi.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class HubConnectionController 
    {
        HubConnection connection;

        /// <summary>
        /// Start HubConnection
        /// </summary>
        /// <returns></returns>
        [HttpGet("Start")]
        public async Task<string> Start()
        {
            var state = JobHub.InitQodelessHub();

            return state.ToString();
        }

        /// <summary>
        /// Stop HubConnection
        /// </summary>
        /// <returns></returns>
        [HttpGet("Stop")]
        public async Task<string> Stop()
        {
            var state = JobHub.StopQodelessHub();

            return state.ToString();
        }
    }
}
