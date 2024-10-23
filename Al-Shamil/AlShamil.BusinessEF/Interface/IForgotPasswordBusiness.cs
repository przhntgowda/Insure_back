using AlShamil.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF.Interface
{
    public interface IForgotPasswordBusiness
    {
        Task<UserDto> FindByEmailAsync(string email);
        Task<bool> IsEmailConfirmedAsync(UserDto userDto);
        Task<bool> CreatePasswordResetTokenAsync(PasswordResetTokenDto passwordResetToken);
        Task<bool> ValidateTokenAsync(PasswordResetTokenDto passwordResetTokenDto);
        Task<bool> UpdatePasswordAsync(UserDto userDto);
    }
}
