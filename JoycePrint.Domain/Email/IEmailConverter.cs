using System.Net.Mail;

namespace JoycePrint.Domain.Business
{
    /// <summary>
    /// Models that are required to send emails have to implement this interface
    /// </summary>
    public interface IEmailConverter
    {
        MailMessage ConvertModelToEmail(IEmail Email);

        string GetMessageBody();

        string GetSubjectLine();

        AttachmentCollection GetMessageAttachments();
    }    
}