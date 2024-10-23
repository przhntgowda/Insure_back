using AlShamil.BusinessEF.Interface;
using AlShamil.Model.Dto;
using AlShamilEntityData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserData _userData;
        public UserBusiness(IUserData userData)
        {
            _userData = userData;
        }
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await _userData.GetUsersAsync();
        }
        public async Task<bool> CreateUserAsync(UserDto userDto)
        {
            return await _userData.CreateUserAsync(userDto);
        }
        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            return await _userData.UpdateUserAsync(userDto);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userData.DeleteUserAsync(id);
        }
    }
}
