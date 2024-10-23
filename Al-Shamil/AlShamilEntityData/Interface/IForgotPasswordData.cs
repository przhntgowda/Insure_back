using AlShamil.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamilEntityData.Interface
{
    public interface IForgotPasswordData
    {
        Task<UserDto> FindByEmailAsync(string email);
        Task<bool> IsEmailConfirmedAsync(UserDto user);
        Task<bool> CreatePasswordResetTokenAsync(PasswordResetTokenDto passwordResetToken);
        Task<bool> ValidateTokenAsync(PasswordResetTokenDto passwordResetTokenDto);
        Task<bool> UpdatePasswordAsync(UserDto user);
    }
}
