using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business.Interface
{
    public interface IUserBusiness<T>
    {
        Task<IEnumerable<T>?> GetUsersAsync();
    }
}
