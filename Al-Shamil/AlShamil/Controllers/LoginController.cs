//using AlShamil.Business.Interface;
using AlShamil.BusinessEF.Interface;
using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using AlShamil.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlShamil.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly ILoginBusiness _loginBusiness;
        IConfiguration _config;
        public LoginController(ILoginBusiness loginBusiness, IConfiguration configuration)
        {
            _loginBusiness = loginBusiness;
            _config = configuration;
        }

        [HttpPost(Name ="UserLogin")]
        
        public async Task<IActionResult> UserLogin(LoginRequest loginRequest)
        {
            LoginDto loginDto = new()
            {
                Email= loginRequest.Email,
                Password= loginRequest.Password,
            };
            UserDto userDto=await _loginBusiness.CheckUserCredentials(loginDto);

            if(userDto != null)
            {
                UserResponse userResponse = new()
                {
                    UserId = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    EmailAddress = userDto.EmailAddress,
                    Password = userDto.Password,
                    PhoneNumber = userDto.PhoneNumber,
                    RoleId = userDto.RoleId,
                    RoleName = userDto.Role.Name,
                };

                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha512Signature
                                    );
                var subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userResponse.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userResponse.EmailAddress ?? ""),
                    new Claim(JwtRegisteredClaimNames.FamilyName,userResponse.LastName ?? ""),
                    new Claim(JwtRegisteredClaimNames.Name,userResponse.FirstName ?? ""),
                    new Claim(ClaimTypes.Role,userResponse.RoleName ?? "")
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);


                return Ok(new { JwtToken = jwtToken });
            }
            return Ok("Credentials Not Found? Register Now.");
        }

        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> Logout(string token)
        //{
            
        //}
    }
}
