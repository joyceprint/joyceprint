using Common.Security.Ciphers;
using System.Configuration;

namespace JoycePrint.Domain.Configuration
{       
    public static class Config
    {        
        public static string RecaptchaUrl => ConfigurationManager.AppSettings.Get("RecaptchaUrl");

        public static string RecaptchaSecretKey => StringCipher.Decrypt(ConfigurationManager.AppSettings.Get("RecaptchaSecretKey"), StringCipher.PassPhrase);

        public static string RecaptchaPublicKey => StringCipher.Decrypt(ConfigurationManager.AppSettings.Get("RecaptchaPublicKey"), StringCipher.PassPhrase);

        public static string NotificationHeaderSuccess => ConfigurationManager.AppSettings.Get("NotificationHeaderSuccess");

        public static string NotificationMessageSuccess => ConfigurationManager.AppSettings.Get("NotificationMessageSuccess");

        public static string NotificationHeaderError => ConfigurationManager.AppSettings.Get("NotificationHeaderError");

        public static string NotificationMessageError => ConfigurationManager.AppSettings.Get("NotificationMessageError");

        public static string QuoteEmail => ConfigurationManager.AppSettings.Get("QuoteEmail");
    }
}