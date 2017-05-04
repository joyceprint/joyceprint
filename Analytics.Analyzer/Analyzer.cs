using Common.Logging;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;
using Common.Logging.Enums;

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
            try
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
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Analyzer initialize method");
            }
        }

        public override void Analyze(HttpContext context)
        {
            try
            {
                if (!Enabled || _trackingId.IsNullOrEmpty()) return;

                var req = (HttpWebRequest)WebRequest.Create(Url);

                req.Method = "POST";
                req.UserAgent = HttpContext.Current.Request.UserAgent;
                req.ContentType = "text/xml";
                req.KeepAlive = false;

                var data = Encoding.ASCII.GetBytes(GetPageTracking(context));

                req.ContentLength = data.Length;
                req.Timeout = _timeout;

                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Analyzer analyze method");
            }
        }

        /// <summary>
        /// Get the page tracking information for the url
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetPageTracking(HttpContext context)
        {
            var pageTracking = new StringBuilder();

            //var page = context.Request.Url.AbsoluteUri;
            var page = "http://joyceprint.dev.com";

            string soapAction;
            if (null != (soapAction = context.Request.Headers["soapaction"]))
            {
                var index = soapAction.LastIndexOf('/');
                page += soapAction.Substring((index == -1 ? 0 : index));
            }

            // Version
            pageTracking.Append($"v={_version}");

            // Tracking Id / Property Id
            pageTracking.Append($"&tid={_trackingId}");

            // Anonymous Client Id
            pageTracking.Append($"&cid={(context.Request.UserHostAddress.IsNullOrEmpty() ? "unknown" : context.Request.UserHostAddress)}");

            // Hit Type [ Type is Page View ]
            pageTracking.Append($"&t=pageview");

            // Document Hostname TODO: Get from the Url
            //pageTracking.Append($"&dp%2F {page.Trim()}");

            // Page
            pageTracking.Append($"&dp%2F {page.Trim()}");

            // Title TODO: Get the title somehow ??
            //pageTracking.Append($"&dt='{page.Trim()}'");

            return pageTracking.ToString();
        }
    }
}