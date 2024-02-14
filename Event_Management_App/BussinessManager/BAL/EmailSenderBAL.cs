using Event_Management_App.BussinessManager.IBAL;
using System.Net.Mail;
using System.Net;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class EmailSenderBAL : IEmailSenderBAL
    {
        private readonly IConfiguration configuration;

        public EmailSenderBAL(IConfiguration configuration)
        {

            this.configuration = configuration;

        }

        public async Task<bool> EmailSendAsync(string email, string Subject, string message)
        {
            bool status = false;
            try
            {
                GetEmailSettingModel getEmailSetting = new GetEmailSettingModel()
                {
                    SecretKey = configuration.GetValue<string>("AppSettings:SecretKey"),
                    From = configuration.GetValue<string>("AppSettings:EmailSettings:From"),
                    SmtpServer = configuration.GetValue<string>("AppSettings:EmailSettings:SmtpServer"),
                    Port = configuration.GetValue<int>("AppSettings:EmailSettings:Port"),
                    EnableSSL = configuration.GetValue<bool>("AppSettings:EmailSettings:EnablSSL"),
                };

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(getEmailSetting.From),
                    Subject = Subject,
                    Body = message
                };
                mailMessage.To.Add(email);
                SmtpClient smtpClient = new SmtpClient(getEmailSetting.SmtpServer)
                {
                    Port = getEmailSetting.Port,
                    Credentials = new NetworkCredential(getEmailSetting.From, getEmailSetting.SecretKey),
                    EnableSsl = getEmailSetting.EnableSSL
                };

                await smtpClient.SendMailAsync(mailMessage);
                status = true;

            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
