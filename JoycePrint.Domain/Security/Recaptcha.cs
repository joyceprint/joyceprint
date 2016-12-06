using System.IO;
using System.Net;
using JoycePrint.Domain.Config;

namespace JoycePrint.Domain.Security
{
    public class Recaptcha
    {
        private string Url { get; }

        private string SecretKey { get; }

        public Recaptcha(IConfig config)
        {
            Url = config.RecaptchaUrl;
            SecretKey = config.RecaptchaSecretKey;
        }

        public string Verify(string captchaResponse)
        {
            var queryString = $"?secret={SecretKey}&response={captchaResponse}";

            var req = (HttpWebRequest)WebRequest.Create(Url + queryString);

            req.Method = "POST";
            req.ContentLength = 0;
            req.Expect = "application/json";

            var res = (HttpWebResponse)req.GetResponse();
            string result;

            using (var streamReader = new StreamReader(res.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }
    }
}