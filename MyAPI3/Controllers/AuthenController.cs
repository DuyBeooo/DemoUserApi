using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyAPI3.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using MyAPI3.Models;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAPI3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthenController(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult UserLogin([FromForm] string acc, [FromForm] string pass)
        {
            var user = _repository.UserLogin(acc, pass);
            if (user != null)
            {
                //create claims details based on the user information
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("UserName", user.Username),
                    new Claim("Password", user.Password),
                    new Claim("Roll", user.Roll.ToString())
                   };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                    claims, expires: DateTime.UtcNow.AddSeconds(60), signingCredentials: signIn);

                var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new LoginRespondFormat()
                {
                    Data = new Data() { AccessToken = tokenHandler, TypeToken = "Bearer" },
                    Error = false,
                    Message = "",
                    StatudeCode = (int)HttpStatusCode.OK
                }
                    );
            }
            return NotFound(new LoginRespondFormat()
            {
                Data = new Data() { AccessToken = "", TypeToken = "Bearer" },
                Error = true,
                Message = "",
                StatudeCode = (int)HttpStatusCode.NotFound
            }
                    );
        }
    }
}
