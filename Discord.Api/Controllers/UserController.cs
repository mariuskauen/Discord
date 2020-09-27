using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Discord.Api.Data;
using Discord.Core.Helpers;
using Discord.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Discord.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MapConfig mapConfig;
        private readonly DataContext context;

        //public UserController(DataContext context, MapConfig mapConfig)
        //{
        //    this.context = context;
        //    this.mapConfig = mapConfig;
        //}
        //[HttpGet("getuser")]
        //public async Task<ActionResult<UserDTO>> GetUser()
        //{
        //    UserDTO user = new UserDTO();
        //    string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var mapper = mapConfig.UserToUserDTO.CreateMapper();
        //    user = mapper.Map<UserDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == userId));

        //    return user;
        //}

    }
}