using System;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;

using JoycePrint.Domain.Models;

namespace JoycePrint.Domain.Business
{
    public class Email
    {
        /// <summary>
        /// Sends an email containing the quote information to the business
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="quoteRequest"></param>
        public void SendEmail(QuoteRequest quoteRequest)
        {
            var smtpConfig = GetSmptConfiguration();

            var smtp = CreateSmtpClient(smtpConfig);

            var message = CreateMailMessage(quoteRequest, smtpConfig);

            // have to ensure the message was sent
            //smtp.Send(message);
        }

        /// <summary>
        /// Gets the SMTP configuration from the web config file
        /// </summary>
        /// <returns></returns>
        private SmtpSection GetSmptConfiguration()
        {
            SmtpSection smtpConfig = null;

            try
            {
                smtpConfig = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            }
            catch (Exception ex)
            {

            }

            return smtpConfig;
        }

        /// <summary>
        /// Creates the SMTP client
        /// </summary>
        /// <returns></returns>
        private SmtpClient CreateSmtpClient(SmtpSection smtpConfig)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = smtpConfig.Network.Host,
                Port = smtpConfig.Network.Port
            };

            return smtp;
        }

        /// <summary>
        /// Creates the mail message
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="quoteRequest"></param>
        /// <param name="smtpConfig"></param>
        /// <returns></returns>
        private MailMessage CreateMailMessage(QuoteRequest quoteRequest, SmtpSection smtpConfig)
        {
            MailMessage message = new MailMessage(smtpConfig.From, quoteRequest.Contact.Email);
            message.Subject = GetEmailSubject(quoteRequest);
            message.Body = quoteRequest.Message;

            return message;
        }

        /// <summary>
        /// Get the email subject for the quote
        /// </summary>
        /// <param name="quoteRequest"></param>
        /// <returns></returns>
        private string GetEmailSubject(QuoteRequest quoteRequest)
        {
            return $"Docket Book Quote : {quoteRequest.Contact.Name}";
        }
    }
}