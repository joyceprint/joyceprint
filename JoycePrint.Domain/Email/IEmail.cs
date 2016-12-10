using System.Net.Configuration;
using System.Net.Mail;

namespace JoycePrint.Domain.Business
{
    public interface IEmail
    {
        SmtpSection smtpConfig { get; }

        bool SendEmail(MailMessage message, SmtpSection smtpConfig);        
    }    
}