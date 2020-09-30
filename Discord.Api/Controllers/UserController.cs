using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Discord.Api.Data;
using Discord.Api.Services;
using Discord.Core.Data;
using Discord.Core.Helpers;
using Discord.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Discord.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MapConfig mapConfig;
        private readonly DataContext context;
        private readonly IMongoDatabase database;
        private readonly UserService _user;

        public UserController(DataContext context, MapConfig mapConfig, IMongoSettings settings, UserService userS)
        {
            this.context = context;
            this.mapConfig = mapConfig;
            _user = userS;

            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }
        [HttpGet("getuser")]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            UserDTO user = new UserDTO();
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mapper = mapConfig.UserToUserDTO.CreateMapper();
            user = mapper.Map<UserDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == userId));

            return user;
        }

        [HttpGet("testget/{query}")]
        public async Task<ActionResult<UserDTO>> TestGen(string query)
        {
            return Ok(await _user.GetUser(query));
        }
    }
}