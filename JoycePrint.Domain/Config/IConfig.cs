namespace JoycePrint.Domain.Config
{
    public interface IConfig
    {        
        string RecaptchaUrl { get; set; }
        
        string RecaptchaSecretKey { get; set; }

        string RecaptchaPublicKey { get; set; }
    }
}
