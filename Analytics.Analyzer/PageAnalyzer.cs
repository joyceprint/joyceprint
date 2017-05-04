using Common.Logging;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using Common.Logging.Enums;

namespace Analytics.Analyzer
{
    public class PageAnalyzer : AnalyzerProvider
    {
        #region Analyzers ARC Map Extra KVP

        private string _version = "1";

        private string _trackingId = "XX-XXXXXXXX-X";

        private int _timeout = 500;

        /// <summary>
        /// This is the tracking type to be used with the analyzer
        /// 
        /// We do this because we need to use different modules for different analytics tracking mechanisms
        /// </summary>
        private TrackingType _trackingType = TrackingType.None;

        protected TrackingType TrackingType => _trackingType;

        #endregion        

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

                if (config["trackingType"] == null)
                    throw new Exception($"Error reading tracking type configuration setting. Default of {_trackingType} will be used on the web site {ApplicationName}");

                Enum.TryParse(config["trackingType"], true, out _trackingType);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Analyzer initialize method");
            }
        }

        public override void Analyze(HttpContext context, TrackingType type)
        {
            try
            {
                if (!Enabled || _trackingId.IsNullOrEmpty() || type != TrackingType) return;
                
                var eventTracking = GetTracking(context);

                SendAnalysis(eventTracking);

            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Analyzer analyze method");
            }
        }        

        /// <summary>
        /// 
        /// </summary>        
        /// <param name="eventTracking"></param>
        private void SendAnalysis(string eventTracking)
        {
            var req = (HttpWebRequest)WebRequest.Create(Url);

            req.Method = "POST";
            req.UserAgent = HttpContext.Current.Request.UserAgent;
            req.ContentType = "text/xml";
            req.KeepAlive = false;

            var data = Encoding.ASCII.GetBytes(eventTracking);

            req.ContentLength = data.Length;
            req.Timeout = _timeout;

            using (var stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// Get the page tracking information for the url
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetTracking(HttpContext context)
        {
            var pageTracking = new StringBuilder();

            // ReSharper disable once RedundantAssignment
            var host = context.Request.Url.AbsoluteUri;

#if DEBUG
            host = "http://joyceprint.dev.com";
#endif
            // TODO: Get the page title somehow?
            var title = "title";

            string page = string.Empty;
            string soapAction;
            if (null != (soapAction = context.Request.Headers["soapaction"]))
            {
                var index = soapAction.LastIndexOf('/');
                page = soapAction.Substring((index == -1 ? 0 : index));
            }

            // Version
            pageTracking.Append($"v={_version}");

            // Tracking Id / Property Id
            pageTracking.Append($"&tid={_trackingId}");

            // Anonymous Client Id
            pageTracking.Append($"&cid={(context.Request.UserHostAddress.IsNullOrEmpty() ? "unknown" : context.Request.UserHostAddress)}");

            // Hit Type [ Type is Page View ]
            pageTracking.Append($"&t=pageview");

            // Document Hostname
            pageTracking.Append($"&dh={host.Trim()}");

            // Page
            pageTracking.Append($"&dp={(page.IsNullOrEmpty() ? "unknown" : page.Trim())}");

            // Title
            pageTracking.Append($"&dt={title.Trim()}");

            return pageTracking.ToString();
        }
    }
}