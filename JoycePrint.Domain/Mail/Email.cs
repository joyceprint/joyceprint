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
        /// Creates the SMTP client
        /// </summary>
        /// <returns></returns>
        private SmtpClient CreateSmtpClient(SmtpSection smtpConfig)
        {
            var smtp = new SmtpClient
            {
                Host = smtpConfig.Network.Host,
                Port = smtpConfig.Network.Port
            };

            return smtp;
        }

        #region Interface Definitions

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
        /// <param name="message">The MailMessage to send</param>
        /// <param name="smtpConfig">The SmptSection to use for configuring the SmtpClient</param>
        /// <returns></returns>
        public bool SendEmail(MailMessage message, SmtpSection smtpConfig)
        {
            var smtpClient = CreateSmtpClient(smtpConfig);
            smtpClient.Send(message);            

            return true;
        }

        #endregion
    }
}