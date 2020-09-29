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
            //5f7f69cb-94dc-404b-978f-00e41dc1ba95
            //string query = "Users:"+id;
            //UserDTO user = await TestGet(new User(), new UserDTO(),query);
            return Ok(await _user.GetUser(query));
        }


        //public async Task<E> TestGet<T,E>(T first, E second,string query) where T : class, new() where E : class, new()
        //{
        //    string[] queries = query.Split(':');

        //    var collection = database.GetCollection<T>(queries[0]);
        //    var filter = Builders<T>.Filter.Eq("_id", queries[1]);

            
        //    //await context.Users.SingleOrDefaultAsync(x => x.Id == "87a4e188-a19e-40ba-894e-03d7637ba24a")
        //    var ret = new E();
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<T,E>());
        //    var mapper = new Mapper(config);
        //    var sour = await collection.Find(filter).FirstOrDefaultAsync();
        //    try
        //    {
        //        return mapper.Map<E>(await collection.Find(filter).FirstOrDefaultAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        string buhu = ex.ToString();
        //    }
        //    return ret;
        //}
    }
}