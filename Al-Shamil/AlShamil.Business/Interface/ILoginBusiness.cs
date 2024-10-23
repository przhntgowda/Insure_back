using AlShamil.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business.Interface
{
    public interface ILoginBusiness<T>
    {
        Task<UserDto> AuthenticateUserAsync(T login);
    }
}
