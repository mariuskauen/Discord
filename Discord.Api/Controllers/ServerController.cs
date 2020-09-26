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
    public class ServerController : ControllerBase
    {
        private readonly ServerService serverService;

        public ServerController(ServerService serverService)
        {
            this.serverService = serverService;
        }

        [HttpPost("createserver/{name}")]
        public async Task<ActionResult> CreateServer(string name)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string status = await serverService.CreateServer(name, userId);
            if (status == "Ok")
                return Ok();
            return BadRequest(status);
        }

        [HttpGet("getmyservers")]
        public async Task<List<ServerListDTO>> GetMyServers()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await serverService.GetMyServers(userId);
        }

        [HttpGet("getserverusers/{serverId}")]
        public async Task<List<SmallUserDTO>> GetServerUsers(string serverId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await serverService.GetServerUsers(serverId);
        }

        [HttpPost("joinserver/{serverId}")]
        public async Task<ActionResult> JoinServer(string serverId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string status = await serverService.JoinServer(userId, serverId);
            if (status == "Ok")
            {
                return Ok();
            }
            else
            {
                return BadRequest(status);
            }
        }
    }
}