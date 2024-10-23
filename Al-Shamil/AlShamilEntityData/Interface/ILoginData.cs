using AlShamil.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamilEntityData.Interface
{
    public interface ILoginData
    {
        Task<UserDto> CheckUserCredentialsAsync(LoginDto loginDto);
    }
}
