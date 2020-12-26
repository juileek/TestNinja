using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TestNinja.Mocking
{
    public interface IEmailSenderBase
    {
        void EmailFile(string emailAddress, string emailBody, string filename, string subject);
    }

    public class EmailSenderBase : IEmailSenderBase
    {
        public  void EmailFile(string emailAddress, string emailBody, string filename, string subject)
        {
            
            var client = new SmtpClient(HousekeeperService.SystemSettingsHelper.EmailSmtpHost)
            {
                Port = HousekeeperService.SystemSettingsHelper.EmailPort,
                Credentials =
                    new NetworkCredential(
                        HousekeeperService.SystemSettingsHelper.EmailUsername,
                        HousekeeperService.SystemSettingsHelper.EmailPassword)
            };

            var from = new MailAddress(HousekeeperService.SystemSettingsHelper.EmailFromEmail, HousekeeperService.SystemSettingsHelper.EmailFromName,
                Encoding.UTF8);
            var to = new MailAddress(emailAddress);

            var message = new MailMessage(@from, to)
            {
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = emailBody,
                BodyEncoding = Encoding.UTF8
            };

            message.Attachments.Add(new Attachment(filename));
            client.Send(message);
            message.Dispose();

            File.Delete(filename);
        }
    }

    
}