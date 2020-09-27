using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Discord.Api.Data;
using Discord.Api.Services;
using Discord.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace Discord.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService auth;
        private readonly IConfiguration config;


        public AuthController(AuthService auth, IConfiguration config)
        {
            this.auth = auth;
            this.config = config;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO vm)
        {
            vm.Username = vm.Username.ToLower();
            if (await auth.UserExists(vm.Username))
                return BadRequest("Username already exists!");
            var userToCreate = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = vm.Username,
                Firstname = "Marius",
                Lastname = "Skauen"
            };

            while (await auth.IdExists(userToCreate.Id))
            {
                userToCreate.Id = Guid.NewGuid().ToString();
            }

            var createdUser = await auth.Register(userToCreate, vm.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO vm)
        {
            LoginResponseDTO response = new LoginResponseDTO();
            var userFromRepo = await auth.Login(vm.Username.ToLower(), vm.Password);
            if (userFromRepo == null)
            {
                response.StatusCode = "Unauthorized";
                return BadRequest();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.StatusCode = "Ok";
            response.Token = tokenHandler.WriteToken(token);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}