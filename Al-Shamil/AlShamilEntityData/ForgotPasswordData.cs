using AlShamil.Model.Dto;
using AlShamilEntityData.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamilEntityData
{
    public class ForgotPasswordData:IForgotPasswordData
    {
        private readonly AlShamilDbContext _dbContext;
        public ForgotPasswordData(AlShamilDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserDto> FindByEmailAsync(string email)
        {
            UserDto? user=await _dbContext.User.SingleOrDefaultAsync(x => x.EmailAddress == email);
            return user;
        }
        public async Task<bool> IsEmailConfirmedAsync(UserDto userDto)
        {
            return await _dbContext.User.AnyAsync(x => x.EmailAddress==userDto.EmailAddress);
        }
        public async Task<bool> CreatePasswordResetTokenAsync(PasswordResetTokenDto passwordResetToken)
        {
            _dbContext.Add(passwordResetToken);
            int r= await _dbContext.SaveChangesAsync();
            return r == 1;

        }
        public async Task<bool> ValidateTokenAsync(PasswordResetTokenDto passwordResetTokenDto)
        {
            PasswordResetTokenDto? resetToken = await _dbContext.PasswordResetToken.SingleOrDefaultAsync(x => x.Token == passwordResetTokenDto.Token && x.UserId == passwordResetTokenDto.UserId);
            TimeSpan timeSpan = resetToken.ExipryDate - DateTime.Now;
            if (timeSpan.Minutes < 60 && timeSpan.Milliseconds > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdatePasswordAsync(UserDto user)
        {
            var existingUser = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Password = user.Password;
                existingUser.ModifiedOn = user.ModifiedOn;
                existingUser.ModifiedBy = user.ModifiedBy;
                _dbContext.Entry(existingUser).State = EntityState.Modified;
                int result= await _dbContext.SaveChangesAsync();
                return result == 1;
            }
            return false;
        }
    }
}
