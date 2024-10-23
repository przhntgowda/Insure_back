using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data.Interface
{
    public interface IUserData<T>
    {
        Task<IEnumerable<T>?> GetAllUsersAsync();
    }
}
