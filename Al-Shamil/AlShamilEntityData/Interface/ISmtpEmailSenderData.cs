using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamilEntityData.Interface
{
    public interface ISmtpEmailSenderData
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
