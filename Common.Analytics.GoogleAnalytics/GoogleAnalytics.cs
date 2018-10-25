using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using Common.Analytics.Tracking;
using Common.Logging;
using Common.Logging.Enums;

namespace Common.Analytics.GoogleAnalytics
{
    public class GoogleAnalytics : AnalyticsProvider
    {
        #region Analyzers ARC Map Extra KVP

        private bool _enabled;

        private string _version = "1";

        private string _trackingId = "XX-XXXXXXXX-X";

        private int _timeout = 500;

        private string _url = "http://www.google-analytics.com/collect";

        #endregion

        /// <summary>
        /// Initialize the provider and read in the extra settings from the provider config
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerConfig"></param>
        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            try
            {
                base.Initialize(providerName, providerConfig);

                if (providerConfig["enabled"] == null)
                    throw new Exception($"Error reading enabled configuration setting. Default of {_enabled} will be used for the provider {providerName}");

                _enabled = bool.Parse(providerConfig["enabled"]);

                if (providerConfig["version"] == null)
                    throw new Exception($"Error reading version configuration setting. Default of {_version} will be used on the web site {ApplicationName}");

                _version = providerConfig["version"];

                if (providerConfig["trackingId"] == null)
                    throw new Exception($"Error reading trackingId configuration setting. Default of {_trackingId} will be used on the web site {ApplicationName}");

                _trackingId = providerConfig["trackingId"];

                if (providerConfig["timeout"] == null)
                    throw new Exception($"Error reading timeout configuration setting. Default of {_timeout} will be used on the web site {ApplicationName}");

                _timeout = int.Parse(providerConfig["timeout"]);

                if (providerConfig["url"] == null)
                    throw new Exception($"Error reading version configuration setting. Default of {_url} will be used on the web site {ApplicationName}");

                _url = providerConfig["url"];
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, $"Error trying to initialize the provider {providerName}");
            }
        }

        /// <summary>
        /// Capture page analysis for the analytics provider
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tracking"></param>
        public override void PageAnalysis(HttpContext context, Page tracking)
        {
            if (!Analyze()) return;

            SendAnalysis(GetPageTracking(context, tracking));
        }

        /// <summary>
        /// Capture event analysis for the analytics provider
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tracking"></param>
        public override void EventAnalysis(HttpContext context, Event tracking)
        {
            if (!Analyze()) return;

            SendAnalysis(GetEventTracking(context, tracking));
        }

        /// <summary>
        /// Check if analysis is to be performed by checking the provider is enabled and it has a tracking id
        /// </summary>
        /// <returns></returns>
        private bool Analyze()
        {
            return _enabled && !string.IsNullOrEmpty(_trackingId);
        }

        /// <summary>
        /// Send the analysis information
        /// </summary>
        /// <param name="tracking"></param>
        private void SendAnalysis(string tracking)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(_url);

                req.Method = "POST";
                req.UserAgent = HttpContext.Current.Request.UserAgent;
                req.ContentType = "text/xml";
                req.KeepAlive = false;

                var data = Encoding.ASCII.GetBytes(tracking);

                req.ContentLength = data.Length;
                req.Timeout = _timeout;

                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, $"Error running analysis for tracking [{tracking}]");
            }
        }

        /// <summary>
        /// Get the page tracking data for analysis
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pageTracking"></param>
        /// <returns></returns>
        private string GetPageTracking(HttpContext context, Page pageTracking)
        {
            var tracking = new StringBuilder();

            // Version
            tracking.Append($"v={_version}");

            // Tracking Id / Property Id
            tracking.Append($"&tid={_trackingId}");

            // Anonymous Client Id
            tracking.Append($"&cid={(string.IsNullOrEmpty(context.Request.UserHostAddress) ? "unknown" : context.Request.UserHostAddress)}");

            // Hit Type [ Type is Page View ]
            tracking.Append($"&t=pageview");

            // Document Hostname
            tracking.Append($"&dh={pageTracking.Host}");

            // Page
            tracking.Append($"&dp={pageTracking.Name}");

            // Title
            if (!string.IsNullOrEmpty(pageTracking.Title))
                tracking.Append($"&dt={pageTracking.Title}");

            return tracking.ToString();
        }

        /// <summary>
        /// Get the event tracking data for analysis
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventTracking"></param>
        /// <returns></returns>
        private string GetEventTracking(HttpContext context, Event eventTracking)
        {
            var tracking = new StringBuilder();

            // Version
            tracking.Append($"v={_version}");

            // Tracking Id / Property Id
            tracking.Append($"&tid={_trackingId}");

            // Anonymous Client Id
            tracking.Append($"&cid={(string.IsNullOrEmpty(context.Request.UserHostAddress) ? "unknown" : context.Request.UserHostAddress)}");

            // Hit Type [ Type is Event ]
            tracking.Append($"&t=event");

            // Event Category [ Required ]
            tracking.Append($"&ec={eventTracking.Category}");

            // Event Action [ Required ]
            tracking.Append($"&ea={eventTracking.Action}");

            // Event Label
            tracking.Append($"&el={eventTracking.Label}");

            // Event Value [ Monetary value associated with the event ]
            tracking.Append($"&ev={eventTracking.Value}");

            // Document Hostname
            tracking.Append($"&dh={eventTracking.Host}");

            return tracking.ToString();
        }
    }
}