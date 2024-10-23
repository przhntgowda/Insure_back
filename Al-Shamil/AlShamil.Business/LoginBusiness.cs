using AlShamil.Business.Interface;
using AlShamil.Data.Interface;
using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business
{
    public class LoginBusiness<T>:ILoginBusiness<T>
    {
        private readonly ILoginData<T> _loginData;
        public LoginBusiness(ILoginData<T> loginData)
        {
            _loginData = loginData;
        }
        public async Task<UserDto> AuthenticateUserAsync(T login)
        {
            return await _loginData.CheckUserCredentialsAsync(login);
        }
    }
}
