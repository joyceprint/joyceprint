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
    }
}
