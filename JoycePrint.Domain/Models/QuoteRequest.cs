using System;
using System.Text;
using System.Net.Mail;
using JoycePrint.Domain.Mail;

namespace JoycePrint.Domain.Models
{
    public class QuoteRequest : IEmailConverter
    {
        /// <summary>
        /// The client contact information that requested the quote
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// The docket book information for the quote
        /// </summary>
        public Enquiry Enquiry { get; set; }

        /// <summary>
        /// The docket book information for the quote
        /// </summary>
        public DocketBook DocketBook { get; set; }        

        public QuoteRequest()
        {
            Contact = new Contact();
            DocketBook = new DocketBook();
            Enquiry = new Enquiry();
        }

        public void SendEmail()
        {
            IEmail email = new Email();
            email.SendEmail(ConvertModelToEmail(email), email.SmtpConfig);
        }

        #region Interface Definitions

        public MailMessage ConvertModelToEmail(IEmail email)
        {
            var message = new MailMessage(Contact.Email, email.SmtpConfig.From)
            {
                Body = GetMessageBody(),
                Subject = GetSubjectLine()
            };

            return message;
        }

        public string GetMessageBody()
        {
            var messageBody = new StringBuilder();

            messageBody.Append("<h1>Client Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Company<strong></dt>");
            messageBody.Append($"<dd>{Contact.Company}</dd>");
            messageBody.Append("<dt><strong>Position</strong></dt>");
            //messageBody.Append($"<dd>{Contact?.Position}</dd>");
            messageBody.Append("<dt><strong>Name</strong></dt>");
            messageBody.Append($"<dd>{Contact.Name}</dd>");
            messageBody.Append("<dt><strong>Telephone</strong></dt>");
            messageBody.Append($"<dd>{Contact.Phone}</dd>");
            messageBody.Append("<dt><strong>Email</strong></dt>");
            messageBody.Append($"<dd>{Contact.Email}</dd>");
            messageBody.Append("</dl>");
            messageBody.Append("<h1>Product Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Docket Type</strong></dt>");
            messageBody.Append($"<dd>{DocketBook.Type}</dd>");
            messageBody.Append("<dt><strong>Docket Size</strong></dt>");
            messageBody.Append($"<dd>{DocketBook.Size}</dd>");
            messageBody.Append("<dt><strong>Quantity</strong></dt>");
            messageBody.Append($"<dd>{DocketBook.Quantity}</dd>");
            messageBody.Append("</dl>");
            messageBody.Append($"<div><strong>User message</strong><p>{Enquiry.Message}</p></div>");

            return messageBody.ToString();
        }

        public string GetSubjectLine()
        {
            return $"Docket Book Quote : {Contact.Name}";
        }

        public AttachmentCollection GetMessageAttachments()
        {
            throw new NotImplementedException("Attachments are not currently unavailable on the site");
        }

        #endregion
    }
}