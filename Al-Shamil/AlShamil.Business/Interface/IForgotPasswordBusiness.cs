using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business.Interface
{
    public interface IForgotPasswordBusiness<T>
    {
        Task<T> FindByEmailAsync(string email);
        Task<bool> IsEmailConfirmedAsync(T user);
        Task<bool> UpdatePasswordAsync(T user);
    }
}
