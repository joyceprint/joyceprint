using System.Collections.Generic;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using Common.Logging;
using Common.Logging.Enums;
using JoycePrint.Domain.Configuration;

namespace JoycePrint.Domain.Mail
{
    public class Email : IEmail
    {
        /// <summary>
        /// 
        /// </summary>
        public const string EmailView = "Email";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public Email(string subject, string body)
        {
            Body = body;
            Subject = subject;
        }

        /// <summary>
        /// Creates the SMTP client
        /// </summary>
        /// <returns></returns>
        private static SmtpClient CreateSmtpClient(SmtpSection smtpConfig)
        {
            var smtp = new SmtpClient
            {
                Host = smtpConfig.Network.Host,
                Port = smtpConfig.Network.Port,
                Credentials = new NetworkCredential(smtpConfig.Network.UserName, Config.DecryptedEmailPassword),
            };
            
            return smtp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private MailMessage CreateMailMessage()
        {
            var emailTo = Config.QuoteEmail;

            var message = new MailMessage(Config.SmtpConfig.From, emailTo)
            {
                Body = Body,
                Subject = Subject,
                IsBodyHtml = true                                              
            };

            foreach (var attachment in Attachments)            
                message.Attachments.Add(attachment);            

            return message;
        }

        #region Interface Definitions

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Attachment> Attachments { get; set; }
       
        /// <summary>
        /// Send the email message using the smtp configuration
        /// </summary>                
        /// <returns></returns>
        public bool SendEmail()
        {                        
            var smtpClient = CreateSmtpClient(Config.SmtpConfig);

            var message = CreateMailMessage();            

            Logger.Instance.Log(MessageLevel.Information, $"FROM : {message.From} - TO : {message.To[0].Address} - HOST : {smtpClient.Host} - USER : {Config.SmtpConfig.Network.UserName} - PASS [Encrypted] : {Config.DecryptedEmailPassword}");

            smtpClient.Send(message);

            return true;
        }

        #endregion
    }
}