using AlShamil.Business.Interface;
using AlShamil.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business
{
    public class ForgotPasswordBusiness<T>:IForgotPasswordBusiness<T> where T : class
    {
        private readonly IForgotPasswordData<T> _forgotPasswordData;

        public ForgotPasswordBusiness(IForgotPasswordData<T> forgotPasswordData)
        {
            _forgotPasswordData = forgotPasswordData;
        }
        public async Task<T> FindByEmailAsync(string email)
        {
            return await _forgotPasswordData.FindByEmailAsync(email);
        }
        public async Task<bool> IsEmailConfirmedAsync(T user)
        {
            return await _forgotPasswordData.IsEmailConfirmedAsync(user);
        }
        public async Task<bool> UpdatePasswordAsync(T user)
        {
            return await _forgotPasswordData.UpdatePasswordAsync(user);
        }
    }
}
