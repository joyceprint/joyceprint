using System.Configuration;

namespace JoycePrint.Domain.Configuration
{       
    public static class Config
    {
        public static string RecaptchaUrl => ConfigurationManager.AppSettings.Get("RecaptchaUrl");

        public static string RecaptchaSecretKey => ConfigurationManager.AppSettings.Get("RecaptchaSecretKey");

        public static string RecaptchaPublicKey => ConfigurationManager.AppSettings.Get("RecaptchaPublicKey");

        public static string NotificationHeaderSuccess => ConfigurationManager.AppSettings.Get("NotificationHeaderSuccess");

        public static string NotificationMessageSuccess => ConfigurationManager.AppSettings.Get("NotificationMessageSuccess");

        public static string NotificationHeaderError => ConfigurationManager.AppSettings.Get("NotificationHeaderError");

        public static string NotificationMessageError => ConfigurationManager.AppSettings.Get("NotificationMessageError");

        public static string QuoteEmail => ConfigurationManager.AppSettings.Get("QuoteEmail");
    }
}