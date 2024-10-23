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
    public class LoginData:ILoginData
    {
        private readonly AlShamilDbContext _dbContext;
        public LoginData(AlShamilDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserDto> CheckUserCredentialsAsync(LoginDto loginDto)
        {
            try
            {
                UserDto? user = await _dbContext.User.Include(x => x.Role).SingleOrDefaultAsync(x => x.EmailAddress == loginDto.Email && x.Password == loginDto.Password);
                if (user != null)
                {
                    return user;
                }
            }
            catch
            {
                throw;
            }
            return null;
        }
    }
}
