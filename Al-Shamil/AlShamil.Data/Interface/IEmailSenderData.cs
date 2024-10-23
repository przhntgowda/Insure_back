using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data.Interface
{
    public interface IEmailSenderData<T>
    {
        Task<bool> SendEmailAsync(T email, T subject, T htmlMessage);
    }
}
