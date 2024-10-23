using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data.Interface
{
    public interface IForgotPasswordData<T> where T : class
    {
        Task<T> FindByEmailAsync(string email);
        Task<bool> IsEmailConfirmedAsync(T user);
        Task<bool> UpdatePasswordAsync(T user);
    }
}
