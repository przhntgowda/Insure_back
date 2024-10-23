using AlShamil.Data.Interface;
using AlShamil.Model.Dto;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data
{
    public class EmailSenderData<T>:IEmailSenderData<T>
    {
        private readonly SmtpSettingsData _smtpSettingsData;
        public EmailSenderData(IOptions<SmtpSettingsData> smtpSettingsData)
        {
            _smtpSettingsData = smtpSettingsData.Value;
        }

        public async Task<bool> SendEmailAsync(T email,T subject,T htmlMessage)
        {
            try
            {
                using (var client = new SmtpClient(_smtpSettingsData.Server, _smtpSettingsData.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpSettingsData.Username, _smtpSettingsData.Password);
                    client.EnableSsl = true;

                    var mailMessage  = new MailMessage()
                    {
                        From=new MailAddress(_smtpSettingsData.Username),
                        Subject=subject.ToString(),
                        Body=htmlMessage.ToString(),
                        IsBodyHtml=true
                    };
                    mailMessage.To.Add(new MailAddress(email.ToString()));
                    await client.SendMailAsync(mailMessage);
                    return true;
                }

            }
            catch
            {
                return false;
                throw;
            }
        }
    }
}
