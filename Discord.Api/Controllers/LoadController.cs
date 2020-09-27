using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Discord.Api.Services;
using Discord.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discord.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadController : ControllerBase
    {
        private readonly LoadService load;

        public LoadController(LoadService load)
        {
            this.load = load;
        }

        [HttpGet("firstload")]
        public async Task<ActionResult<FirstLoadDTO>> FirstLoad()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await load.FirstLoad(userId);
        }

        [HttpGet("loadserver/{serverId}")]
        public async Task<ActionResult<ServerDTO>> LoadServer(string serverId)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(await load.LoadServer(serverId));
        }

        //FLYTTET TIL CHANNELCONTROLLER
        //[HttpGet("loadchannel/{channelId}")]
        //public async Task<ActionResult<FirstLoadDTO>> LoadChannel(string channelId)
        //{
        //    string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    return Ok(await load.LoadChannel(channelId));
        //}

        //[HttpGet("loadconversation")]
        //public async Task<ActionResult<FirstLoadDTO>> LoadConversation()
        //{
        //    string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //}
    }
}