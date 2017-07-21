using System;
using System.IO;
using System.Net;
using Common.Logging;
using Common.Logging.Enums;
using JoycePrint.Configuration;

namespace JoycePrint.Domain.Security
{
    public class Recaptcha
    {
        private string Url { get; }

        private string SecretKey { get; }

        public Recaptcha()
        {
            Url = Config.RecaptchaUrl;
            SecretKey = Config.RecaptchaSecretKey;
        }
        
        public string Verify(string captchaResponse)
        {
            var result = string.Empty;

            try
            {
                var queryString = $"?secret={SecretKey}&response={captchaResponse}";

                var req = (HttpWebRequest) WebRequest.Create(Url + queryString);

                req.Method = "POST";
                req.ContentLength = 0;
                req.Expect = "application/json";

                var res = (HttpWebResponse) req.GetResponse();
                var responseStream = res.GetResponseStream();

                if (responseStream == null) return result;

                using (var streamReader = new StreamReader(responseStream))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Recaptcha verification failure");
            }

            return result;
        }
    }
}