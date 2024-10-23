using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF.Interface
{
    public interface ISmtpEmailSenderBusiness
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
