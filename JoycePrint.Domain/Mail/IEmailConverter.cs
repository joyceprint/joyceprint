using System.Net.Mail;

namespace JoycePrint.Domain.Mail
{
    /// <summary>
    /// Models that are required to send emails have to implement this interface
    /// </summary>
    public interface IEmailConverter
    {
        MailMessage ConvertModelToEmail(IEmail email);

        string GetMessageBody();

        string GetSubjectLine();

        AttachmentCollection GetMessageAttachments();
    }    
}