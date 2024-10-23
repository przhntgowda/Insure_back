using AlShamil.Business.Interface;
using AlShamil.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business
{
    public class EmailSenderBusiness<T>:IEmailSenderBusiness<T>
    {
        private readonly IEmailSenderData<T> _emailSenderData;
        public EmailSenderBusiness(IEmailSenderData<T> emailSenderData)
        {
            _emailSenderData = emailSenderData;
        }
        public async Task<bool> SendEmailAsync(T email,T subject,T htmlMessage)
        {
            return await _emailSenderData.SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
