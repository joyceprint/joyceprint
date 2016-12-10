using System;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;

using JoycePrint.Domain.Models;

namespace JoycePrint.Domain.Business
{
    public class Email : IEmail
    {
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

        #region Interface Definitions

        /// <summary>
        /// Get the smtp configuration section from the web config file
        /// </summary>
        public SmtpSection smtpConfig
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
                    // Log the exception using the ILog
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
            var emailSent = false;
            SmtpClient smtpClient = null;

            try
            {
                smtpClient = CreateSmtpClient(smtpConfig);
                smtpClient.Send(message);
                emailSent = true;
            }
            catch (Exception ex)
            {
                // Log the exception using ILog
            }

            return emailSent;
        }

        #endregion
    }
}