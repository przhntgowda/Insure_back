using AlShamil.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF.Interface
{
    public interface IUserBusiness
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<bool> CreateUserAsync(UserDto userDto);
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
