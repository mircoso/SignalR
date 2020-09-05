using HubServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotController : ControllerBase
    {
        private readonly IHubContext<SpotHub, ISpot> _spotHub;

        public SpotController(IHubContext<SpotHub, ISpot> hubContext)
        {
            _spotHub = hubContext;
        }

        [HttpGet]
        public async Task<string> Only()//[FromForm] string message
        {
            await _spotHub.Clients.All.OnlySpot();
            return "OK";
        }
    }
}