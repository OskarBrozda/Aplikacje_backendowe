using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolSchedule.Infrastructure.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Domain.Model;
using WebApi.Repository;

namespace SchoolSchedule.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly IOptions<JwtConfig> _config;
        private readonly IUserRepository _repo;
        public SecurityController(IOptions<JwtConfig> config, IUserRepository repo)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] WebApiUser user)
        {

            if (_repo.AuthorizeUser(user.UserName, user.Password))
            {
                var issuer = _config.Value.Issuer;
                var audience = _config.Value.Audience;
                var key = Encoding.ASCII.GetBytes(_config.Value.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                 new Claim("Id", Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();
        }

    }
}
