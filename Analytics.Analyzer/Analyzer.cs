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
        private string _version = "1";

        private string _trackingId = Config.AnalyticsTrackingId;

        private int _timeout = 500;

        private static readonly string ApplicationName = HostingEnvironment.SiteName;

        private const string Url = "http://www.google-analytics.com/collect";

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            if (config["version"] == null)
                throw new Exception($"Error reading version configuration setting. Default of {_version} will be used on the web site {ApplicationName}");

            _version = config["version"];

            if (config["trackingId"] == null)
                throw new Exception($"Error reading trackingId configuration setting. Default of {_trackingId} will be used on the web site {ApplicationName}");

            _trackingId = config["trackingId"];

            if (config["timeout"] == null)
                throw new Exception($"Error reading timeout configuration setting. Default of {_timeout} will be used on the web site {ApplicationName}");

            _timeout = int.Parse(config["timeout"]);
        }

        public override void Analyze(HttpContext context)
        {
            if (!Enabled || _trackingId.IsNullOrEmpty()) return;

            var req = (HttpWebRequest)WebRequest.Create(Url);

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

            var data = Encoding.ASCII.GetBytes($"v={_version}" +
                                               $"&tid={_trackingId}" +
                                               $"&cid={(context.Request.UserHostAddress.IsNullOrEmpty() ? "unknown" : context.Request.UserHostAddress)}" +
                                               $"&t=pageview" +
                                               $"&dp%2F {page.Trim()}");

            req.ContentLength = data.Length;
            req.Timeout = _timeout;

            using (var stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }
    }
}