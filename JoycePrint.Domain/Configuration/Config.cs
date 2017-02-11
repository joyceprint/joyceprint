using System.Configuration;

namespace JoycePrint.Domain.Configuration
{       
    public static class Config
    {
        public static string RecaptchaUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("RecaptchaUrl");
            }            
        }

        public static string RecaptchaSecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("RecaptchaSecretKey");
            }
        }
        
        public static string RecaptchaPublicKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("RecaptchaPublicKey");
            }           
        }

        public static string NotificationHeaderSuccess
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("NotificationHeaderSuccess");
            }
        }

        public static string NotificationMessageSuccess
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("NotificationMessageSuccess");
            }
        }

        public static string NotificationHeaderError
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("NotificationHeaderError");
            }
        }

        public static string NotificationMessageError
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("NotificationMessageError");
            }
        }
    }
}
