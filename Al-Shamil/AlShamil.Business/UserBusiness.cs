using AlShamil.Business.Interface;
using AlShamil.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business
{
    public class UserBusiness<T>:IUserBusiness<T>
    {
        private readonly IUserData<T> _userData;
        public UserBusiness(IUserData<T> userData)
        {
            _userData = userData;
        }
        public async Task<IEnumerable<T>?> GetUsersAsync()
        {
            IEnumerable<T>? users=await _userData.GetAllUsersAsync();
            return users;
        }
    }
}
