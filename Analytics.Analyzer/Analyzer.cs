using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using Common.Logging;
using Common.Logging.Enums;

namespace Analytics.Analyzer
{
    public abstract class Analyzer : AnalyzerProvider
    {
        #region Analyzers ARC Map Extra KVP

        protected string Version = "1";

        protected string TrackingId = "XX-XXXXXXXX-X";

        protected int Timeout = 500;

        /// <summary>
        /// This is the tracking type to be used with the analyzer
        /// 
        /// We do this because we need to use different modules for different analytics tracking mechanisms
        /// </summary>
        private TrackingType _trackingType = TrackingType.None;

        public TrackingType TrackingType => _trackingType;

        #endregion       

        protected const string Url = "http://www.google-analytics.com/collect";

        public override void Initialize(string name, NameValueCollection config)
        {
            try
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

                if (config["trackingType"] == null)
                    throw new Exception($"Error reading tracking type configuration setting. Default of {_trackingType} will be used on the web site {ApplicationName}");

                Enum.TryParse(config["trackingType"], true, out _trackingType);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Analyzer initialize method");
            }
        }

        /// <summary>
        /// 
        /// </summary>        
        /// <param name="eventTracking"></param>
        protected void SendAnalysis(string eventTracking)
        {
            var req = (HttpWebRequest)WebRequest.Create(Url);

            req.Method = "POST";
            req.UserAgent = HttpContext.Current.Request.UserAgent;
            req.ContentType = "text/xml";
            req.KeepAlive = false;

            var data = Encoding.ASCII.GetBytes(eventTracking);

            req.ContentLength = data.Length;
            req.Timeout = Timeout;

            using (var stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }
    }
}