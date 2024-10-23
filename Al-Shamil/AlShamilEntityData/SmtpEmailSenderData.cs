using AlShamil.Model.Dto;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AlShamilEntityData.Interface;

namespace AlShamilEntityData
{
    public class SmtpEmailSenderData: ISmtpEmailSenderData
    {
        private readonly SmtpSettingsData _smtpSettingsData;
        public SmtpEmailSenderData(IOptions<SmtpSettingsData> smtpSettingsData)
        {
            _smtpSettingsData = smtpSettingsData.Value;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                using (var client = new SmtpClient(_smtpSettingsData.Server, _smtpSettingsData.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_smtpSettingsData.Username, _smtpSettingsData.Password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage()
                    {
                        From = new MailAddress(_smtpSettingsData.Username),
                        Subject = subject.ToString(),
                        Body = htmlMessage.ToString(),
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(email);
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
