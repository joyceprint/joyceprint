using JoycePrint.Domain.Configuration;

namespace JoycePrint.Domain.Models
{
    public class Security
    {
        public string RecaptchaUrl => Config.RecaptchaUrl;

        public string RecaptchaSecretKey => Config.RecaptchaSecretKey;

        public string RecaptchaPublicKey => Config.RecaptchaPublicKey;
    }
}