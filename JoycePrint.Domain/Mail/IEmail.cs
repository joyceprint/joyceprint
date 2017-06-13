using System.Net.Configuration;
using System.Net.Mail;

namespace JoycePrint.Domain.Mail
{
    public interface IEmail
    {
        string Body { get; set; }

        string Subject { get; set; }

        SmtpSection SmtpConfig { get; }

        bool SendEmail();                
    }    
}