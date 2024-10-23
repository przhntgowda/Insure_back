using AlShamil.Model.Dto;
using AlShamil.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data.Interface
{
    public interface ILoginData<T>
    {
        Task<UserDto> CheckUserCredentialsAsync(T login);
    }
}
