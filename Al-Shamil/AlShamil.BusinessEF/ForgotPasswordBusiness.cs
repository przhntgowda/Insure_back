using AlShamil.BusinessEF.Interface;
using AlShamil.Model.Dto;
using AlShamilEntityData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF
{
    public class ForgotPasswordBusiness: IForgotPasswordBusiness
    {
        private readonly IForgotPasswordData _forgotPasswordData;
        public ForgotPasswordBusiness(IForgotPasswordData forgotPasswordData)
        {
            _forgotPasswordData = forgotPasswordData;
        }
        public async Task<UserDto> FindByEmailAsync(string email)
        {
            return await _forgotPasswordData.FindByEmailAsync(email);
        }
        public async Task<bool> IsEmailConfirmedAsync(UserDto userDto)
        {
            return await _forgotPasswordData.IsEmailConfirmedAsync(userDto);
        }
        public async Task<bool> CreatePasswordResetTokenAsync(PasswordResetTokenDto passwordResetToken)
        {
            return await _forgotPasswordData.CreatePasswordResetTokenAsync(passwordResetToken);
        }
        public async Task<bool> ValidateTokenAsync(PasswordResetTokenDto passwordResetTokenDto)
        {
            return await _forgotPasswordData.ValidateTokenAsync(passwordResetTokenDto);
        }
        public async Task<bool> UpdatePasswordAsync(UserDto userDto)
        {
            return await _forgotPasswordData.UpdatePasswordAsync(userDto);
        }
    }
}
