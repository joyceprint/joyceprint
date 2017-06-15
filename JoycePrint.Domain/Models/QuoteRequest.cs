using System;
using System.Collections.Generic;
using System.Net.Mail;
using Common.Logging;
using Common.Logging.Enums;
using JoycePrint.Domain.Mail;
using System.Web;

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

        /// <summary>
        /// The attachment collection to attach to the email
        /// </summary>        
        public Attachment Attachment { get; set; }

        public QuoteRequest()
        {
            Contact = new Contact();
            DocketBook = new DocketBook();
            Enquiry = new Enquiry();
            Attachment = new Attachment();
        }

        public bool SendEmail(string emailBody, HttpContextBase httpContext)
        {
            try
            {
                var inlineEmailBody = ConvertToCssInline(emailBody, httpContext);

                IEmail email = new Email(GetSubjectLine(), inlineEmailBody);

                email.Attachments = GetMessageAttachments();

                return email.SendEmail();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex);
                return false;
            }
        }

        /// <summary>
        /// Converts the email style classes to inline styles.
        /// 
        /// This uses a 3rd party tool - PreMailer.Net
        ///     https://github.com/milkshakesoftware/PreMailer.Net
        ///     https://www.nuget.org/packages/PreMailer.Net/
        /// </summary>
        /// <param name="httpContext">The httpContext of the request</param>
        /// <param name="path">A path to a css file to inline</param>
        /// <param name="bodyToConvert">The body of the email to apply the inlined styles to</param>
        /// <returns>An inline styled email</returns>
        /// <remarks>
        /// Calling the MoveCssInline doesn't seem to apply the styles specified in the css file
        /// 
        /// It seems to apply the bootstrap and locator less styles that are loaded on the page in the bundle
        /// In this case [~/Content/css/locator/locatoremail]
        /// 
        /// Removing the bundle from the Layout view and trying to apply the css here doesn't seem to work
        /// </remarks>
        private static string ConvertToCssInline(string bodyToConvert, HttpContextBase httpContext, string path = null)
        {
            if (httpContext == null) return null;
            if (httpContext.Request == null) return null;
            if (httpContext.Request.Url == null) return null;

            var uri = new Uri(httpContext.Request.Url.Scheme + "://" + httpContext.Request.Url.Authority, UriKind.Absolute);

            // The Uri needs to be the state of the path that the bundle will use
            var result = PreMailer.Net.PreMailer.MoveCssInline(uri, bodyToConvert);

            return result.Html;
        }

        private string GetSubjectLine()
        {
            return $"Docket Book Quote : {Contact.Name}";
        }

        private List<System.Net.Mail.Attachment> GetMessageAttachments()
        {
            var attachments = new List<System.Net.Mail.Attachment>();

            foreach (var file in Attachment.Files)
            {
                attachments.Add(new System.Net.Mail.Attachment(file.InputStream, file.FileName));
            }     
            
            return attachments;           
        }        
    }
}