using BlindBoxSS.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlindBoxSS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IAccountService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/Login")]
        public async Task<ActionResult<Account>> Login([FromBody] LoginDTO user)
        {
            var userInDb = await _userService.AuthenticateAsync(user.Username, user.Password);
            if (userInDb != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Username", userInDb.Email),
                    new Claim("Password", userInDb.Password),
                    new Claim("roleId", userInDb.Role.ToString()) // Đảm bảo roleId được thêm vào
                };


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signIn
                );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { token = tokenString, User = user });
            }
            return BadRequest("Invalid credentials");
        }
    }
}

