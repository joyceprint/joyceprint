using System.IO;
using System.Net;
using System.Web.Mvc;

using JoycePrint.Domain.Configuration;

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

        [HttpGet]
        public string Verify(string captchaResponse)
        {
            var queryString = $"?secret={SecretKey}&response={captchaResponse}";

            var req = (HttpWebRequest) WebRequest.Create(Url + queryString);

            req.Method = "POST";
            req.ContentLength = 0;
            req.Expect = "application/json";

            var res = (HttpWebResponse) req.GetResponse();
            var result = string.Empty;
            var responseStream = res.GetResponseStream();

            if (responseStream == null) return result;

            using (var streamReader = new StreamReader(responseStream))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }
    }
}