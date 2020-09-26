using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Discord.Api.Data;
using Discord.Api.Services;
using Discord.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Discord.Api.Controllers
{
    [Route("api/relation")]
    [Authorize]
    [ApiController]
    public class RelationController : ControllerBase
    {
        private readonly RelationService relation;
        private readonly DataContext context;

        public RelationController(RelationService relation, DataContext context)
        {
            this.relation = relation;
            this.context = context;
        }

        [HttpPost("sendrequest/{receiverId}")]
        public async Task<IActionResult> SendFriendRequest(string receiverId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return BadRequest("Who are you? O_o o_O");

            if (!await context.Users.AsNoTracking().AnyAsync(x => x.Id == receiverId))
                return BadRequest("No such user, dude!");

            string status = await relation.SendRequest(user, receiverId);
            if(status == "Ok")
            {
                return Ok();
            }
            else
            {
                return BadRequest(status);
            }
        }

        [HttpGet("getrequests")]
        public async Task<ActionResult<List<FriendRequest>>> GetRequests()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await relation.GetFriendRequests(userId);
        }

        [HttpGet("getmyfriends")]
        public async Task<ActionResult<List<FriendListDTO>>> GetMyFriends()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await relation.GetMyFriends(userId);
        }

        [HttpPost("acceptrequest/{requestId}")]
        public async Task<ActionResult> AcceptRequest(string requestId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string status = await relation.AcceptRequest(userId, requestId);
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