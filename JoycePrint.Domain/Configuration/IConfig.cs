namespace JoycePrint.Domain.Configuration
{
    public interface IConfig
    {        
        string RecaptchaUrl { get;  }
        
        string RecaptchaSecretKey { get; }

        string RecaptchaPublicKey { get; }
    }
}
