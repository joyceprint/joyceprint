using System.Net.Configuration;
using System.Net.Mail;

namespace JoycePrint.Domain.Mail
{
    public interface IEmail
    {
        SmtpSection SmtpConfig { get; }

        bool SendEmail(MailMessage message, SmtpSection smtpConfig);        
    }    
}