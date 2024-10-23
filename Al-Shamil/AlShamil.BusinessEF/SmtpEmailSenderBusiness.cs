using AlShamil.BusinessEF.Interface;
using AlShamilEntityData;
using AlShamilEntityData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF
{
    public class SmtpEmailSenderBusiness: ISmtpEmailSenderBusiness
    {
        private readonly ISmtpEmailSenderData _smtpEmailSenderData;
        public SmtpEmailSenderBusiness(ISmtpEmailSenderData smtpEmailSenderData)
        {
            _smtpEmailSenderData = smtpEmailSenderData;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return await _smtpEmailSenderData.SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
