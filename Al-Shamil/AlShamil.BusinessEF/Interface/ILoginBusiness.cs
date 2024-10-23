using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF.Interface
{
    public interface ILoginBusiness
    {
        Task<UserDto> CheckUserCredentials(LoginDto loginDto);
    }
}
