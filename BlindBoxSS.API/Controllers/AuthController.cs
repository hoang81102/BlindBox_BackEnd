using BlindBoxSS.API.DTO;
using BlindBoxSS.API.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Services;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace BlindBoxSS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthController(IAccountService userService, IConfiguration configuration, IEmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTO.RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("User data is null");
            }

            try
            {
                var registeredUser = await _userService.RegisterAccountAsync(request.Email, request.Password, request.Name, request.PhoneNumber);
                if (registeredUser == false)
                {
                    return Conflict("Email already exists");
                }
              return Ok("Check your email to Verufy Account");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/Login")]
        public async Task<ActionResult<Account>> Login([FromBody] LoginDTO user)
        {
            var userInDb = await _userService.LoginAsync(user.Username, user.Password);
            if (userInDb != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Username", userInDb.Email),
                    new Claim("Password", userInDb.Password),
                    new Claim("Role", userInDb.Role.ToString()) // Đảm bảo roleId được thêm vào
                };


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? string.Empty));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signIn
                );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { token = tokenString, User = userInDb });
            }
            return BadRequest("Invalid credentials");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]); // Dùng đúng secret key

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                /*var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;*/
                var checkEmail = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (checkEmail == null) return BadRequest("Token không hợp lệ");
                    await _userService.VerifyAccountAsync(checkEmail);
                     return Redirect("http://localhost:5000/welcome");
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi xác thực: {ex.Message}");
            }
        }


        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] DTO.ForgotPasswordRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return Conflict("Email không tồn tại.");
            }

            try
            {
                var token = _emailService.GeneratePasswordResetToken(request.Email);
                await _emailService.SendResetPasswordEmail(request.Email, token);
                return Ok("Vui lòng kiểm tra email để đặt lại mật khẩu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
            }
        }



        [AllowAnonymous]
        [HttpPost("confirm-reset-password")]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] DTO.ResetPasswordRequest request)
        {
            var principal = ValidateToken(request.Token);
            if (principal == null)
            {
                return BadRequest("Invalid or expired token.");
            }

            var email = principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email.");
            }

            try
            {
                await _userService.UpdatePasswordAsync(email, request.NewPassword);
                return Ok("Your password has been reset successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("verify-reset-token")]
        public IActionResult VerifyResetToken([FromQuery] string token)
        {
            var principal = ValidateToken(token);
            if (principal == null)
            {
                return Redirect("http://localhost:5000/somethingwrong");
            }
            return Redirect("http://localhost:5000/resetpassword");
        }


        private ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

    }


}

