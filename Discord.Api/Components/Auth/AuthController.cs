using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Discord.Api.Components.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Discord.Api.Components.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService auth;
        private readonly MediatorService mediator;
        private readonly IConfiguration config;

        public AuthController(AuthService auth, IConfiguration config, MediatorService mediator)
        {
            this.auth = auth;
            this.config = config;
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register reg)
        {
            reg.Username = reg.Username.ToLower();
            if (await auth.UserExists(reg.Username))
                return BadRequest("Username already exists!");
            var userToCreate = new Auth
            {
                Id = Guid.NewGuid().ToString(),
                Username = reg.Username
            };

            while (await auth.IdExists(userToCreate.Id))
            {
                userToCreate.Id = Guid.NewGuid().ToString();
            }

            var createdUser = await auth.Register(userToCreate, reg.Password);
            await mediator.InitializeUser(createdUser.Id, createdUser.Username);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login vm)
        {
            var userFromRepo = await auth.Login(vm.Username.ToLower(), vm.Password);
            if (userFromRepo == null)
            {
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

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}