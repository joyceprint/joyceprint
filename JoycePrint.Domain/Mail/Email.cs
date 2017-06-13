using System;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using Common.Logging;
using Common.Logging.Enums;

namespace JoycePrint.Domain.Mail
{
    public class Email : IEmail
    {
        /// <summary>
        /// 
        /// </summary>
        public static string EmailView = "Email";

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
                Port = smtpConfig.Network.Port
            };

            return smtp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private MailMessage CreateMailMessage()
        {
            var emailTo = Configuration.Config.QuoteEmail;

            var message = new MailMessage(SmtpConfig.From, emailTo)
            {
                Body = Body,
                Subject = Subject,
                IsBodyHtml = true                
            };

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
        /// Get the smtp configuration section from the web config file
        /// </summary>
        public SmtpSection SmtpConfig
        {
            get
            {
                SmtpSection smtpConfig = null;

                try
                {
                    smtpConfig = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(MessageLevel.Error, ex.Message);
                }

                return smtpConfig;
            }
        }

        /// <summary>
        /// Send the email message using the smtp configuration
        /// </summary>                
        /// <returns></returns>
        public bool SendEmail()
        {
            var smtpClient = CreateSmtpClient(SmtpConfig);

            var message = CreateMailMessage();

            Logger.Instance.Log(MessageLevel.Information, $"FROM : {message.From} - TO : {message.To[0].Address} - HOST : {smtpClient.Host} - USER : {SmtpConfig.Network.UserName} - PASS : {SmtpConfig.Network.Password}");

            smtpClient.Send(message);

            return true;
        }

        #endregion
    }
}