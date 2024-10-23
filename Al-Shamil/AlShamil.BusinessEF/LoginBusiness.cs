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
    public class LoginBusiness:ILoginBusiness
    {
        private readonly ILoginData _loginData;
        public LoginBusiness(ILoginData loginData)
        {
            _loginData = loginData;
        }
        public async Task<UserDto> CheckUserCredentials(LoginDto loginDto)
        {
            return await _loginData.CheckUserCredentialsAsync(loginDto);
        }
    }
}
