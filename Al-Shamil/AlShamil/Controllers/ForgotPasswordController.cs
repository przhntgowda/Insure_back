//using AlShamil.Business.Interface;
using AlShamil.BusinessEF;
using AlShamil.BusinessEF.Interface;
using AlShamil.Data.Interface;
using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using AlShamil.Model.Response;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ResetPasswordRequest = AlShamil.Model.Request.ResetPasswordRequest;

namespace AlShamil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IForgotPasswordBusiness _forgotPasswordBusiness;
        private readonly ISmtpEmailSenderBusiness _emailSenderData;

        public ForgotPasswordController(IForgotPasswordBusiness forgotPasswordBusiness, ISmtpEmailSenderBusiness emailSenderData)
        {
            _forgotPasswordBusiness = forgotPasswordBusiness;
            _emailSenderData = emailSenderData;
        }

        [HttpPost("ForgotPasswordAsync")]
        public async Task<IActionResult> ForgotPasswordAsync(string email)
        {
            if (email != null)
            {
                UserDto userDto = await _forgotPasswordBusiness.FindByEmailAsync(email);
                if (userDto == null)
                {
                    return Ok("Email Does Not Exits");
                }
                var token = Guid.NewGuid().ToString();



                string swaggerUrl = "https://localhost:44347/swagger/index.html";
                string callbackUrl = swaggerUrl.Replace("/swagger/index.html", "/callback");

                PasswordResetTokenDto resetTokenDto = new()
                {
                    Token= token,
                    ExipryDate= DateTime.Now.AddMinutes(20),
                    UserId=userDto.Id,
                };
                bool isTrue=await _forgotPasswordBusiness.CreatePasswordResetTokenAsync(resetTokenDto);
                if(isTrue)
                {
                    bool result = await _emailSenderData.SendEmailAsync(email, "Reset Password", callbackUrl);
                    if (result)
                    {
                        ForgotPasswordResponse forgotPasswordResponse = new()
                        {
                            Token = token,
                            Email = userDto.EmailAddress,
                            UserId = userDto.Id,
                        };
                        return Ok(forgotPasswordResponse);
                    }
                }
            }

            return Ok();
        }

        [HttpPost("ResetPasswordAsync")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest resetPassword)
        {
            if (ModelState.IsValid)
            {
                PasswordResetTokenDto resetTokenDto = new()
                {
                    Token=resetPassword.Token,
                    UserId=resetPassword.UserId,
                };
                bool isValid=await _forgotPasswordBusiness.ValidateTokenAsync(resetTokenDto);
                if (isValid)
                {
                    UserDto userDto = new UserDto()
                    {
                        EmailAddress = resetPassword.Email,
                        Password = resetPassword.Password,
                        Id = resetPassword.UserId,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = User.FindFirstValue(JwtRegisteredClaimNames.Name)
                    };
                    bool result = await _forgotPasswordBusiness.UpdatePasswordAsync(userDto);
                    if (result)
                        return Ok(true);
                }
            }
            return Ok("Password Not Updated Successfully");
        }
    }
}
