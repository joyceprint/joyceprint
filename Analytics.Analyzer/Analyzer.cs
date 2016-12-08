using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Analytics.Analyzer
{
    public class Analyzer : AnalyzerProvider
    {
        private string Version = "1";

        private string TrackingId = "UA-88639794-1";

        private int Timeout = 500;

        private static string ApplicationName = HostingEnvironment.SiteName;

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            if (config["version"] == null)
                throw new Exception($"Error reading version configuration setting. Default of {Version} will be used on the web site {ApplicationName}");

            Version = config["version"];

            if (config["trackingId"] == null)
                throw new Exception($"Error reading trackingId configuration setting. Default of {TrackingId} will be used on the web site {ApplicationName}");

            TrackingId = config["trackingId"];

            if (config["timeout"] == null)
                throw new Exception($"Error reading timeout configuration setting. Default of {Timeout} will be used on the web site {ApplicationName}");

            Timeout = int.Parse(config["timeout"]);
        }

        public override void Analyze(HttpContext context)
        {
            if (!Enabled || TrackingId.IsNullOrEmpty()) return;

            var req = (HttpWebRequest)WebRequest.Create("http://www.google-analytics.com/debug/collect");

            var page = context.Request.Url.AbsoluteUri;
            string soapAction;

            if (null != (soapAction = context.Request.Headers["soapaction"]))
            {
                var index = soapAction.LastIndexOf('/');
                page += soapAction.Substring((index == -1 ? 0 : index));
            }

            req.Method = "POST";
            req.UserAgent = HttpContext.Current.Request.UserAgent;
            req.ContentType = "text/xml";
            req.KeepAlive = false;

            var data = Encoding.ASCII.GetBytes($"v={Version}" +
                                               $"&tid={TrackingId}" +
                                               $"&cid={(context.Request.UserHostAddress.IsNullOrEmpty() ? "unknown" : context.Request.UserHostAddress)}" +
                                               $"&t=pageview" +
                                               $"&dp%2F {page.Trim()}");

            req.ContentLength = data.Length;
            req.Timeout = Timeout;

            using (var stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }
    }
}