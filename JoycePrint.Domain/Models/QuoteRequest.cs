using System;
using System.Net.Mail;
using Common.Logging;
using Common.Logging.Enums;
using JoycePrint.Domain.Mail;

namespace JoycePrint.Domain.Models
{
    public class QuoteRequest
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

        public bool SendEmail(string emailBody)
        {
            try
            {
                IEmail email = new Email(GetSubjectLine(), emailBody);                
                return email.SendEmail();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex);
                return false;
            }
        }
        
        public string GetSubjectLine()
        {
            return $"Docket Book Quote : {Contact.Name}";
        }

        public AttachmentCollection GetMessageAttachments()
        {
            throw new NotImplementedException("Attachments are not currently unavailable on the site");
        }        
    }
}