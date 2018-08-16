using System;
using System.Configuration;
using System.Net.Configuration;
using Common.Logging;
using Common.Logging.Enums;
using Common.Security.Ciphers;

namespace JoycePrint.Domain.Configuration
{       
    public static class Config
    {        
        public static string RecaptchaUrl => ConfigurationManager.AppSettings.Get("RecaptchaUrl");

        public static string RecaptchaSecretKey => StringCipher.Decrypt(ConfigurationManager.AppSettings.Get("RecaptchaSecretKey"), PassPhrase);

        public static string RecaptchaPublicKey => StringCipher.Decrypt(ConfigurationManager.AppSettings.Get("RecaptchaPublicKey"), PassPhrase);

        public static string NotificationHeaderSuccess => ConfigurationManager.AppSettings.Get("NotificationHeaderSuccess");

        public static string NotificationMessageSuccess => ConfigurationManager.AppSettings.Get("NotificationMessageSuccess");

        public static string NotificationHeaderError => ConfigurationManager.AppSettings.Get("NotificationHeaderError");

        public static string NotificationMessageError => ConfigurationManager.AppSettings.Get("NotificationMessageError");

        public static string QuoteEmail => ConfigurationManager.AppSettings.Get("QuoteEmail");

        private static string PassPhrase => ConfigurationManager.AppSettings.Get("PassPhrase");

        /// <summary>
        /// Get the smtp configuration section from the web config file
        /// </summary>
        public static SmtpSection SmtpConfig
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
        /// Decrypt the email password to be used by the Email class
        /// </summary>
        public static string DecryptedEmailPassword => StringCipher.Decrypt(SmtpConfig.Network.Password, PassPhrase);
    }
}