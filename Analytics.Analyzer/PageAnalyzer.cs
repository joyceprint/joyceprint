using Common.Logging;
using System;
using System.Text;
using System.Web;
using Analytics.Enums;
using Common.Logging.Enums;

namespace Analytics.Analyzer
{
    public class PageAnalyzer : Analyzer
    {
        public override void Analyze(HttpContext context, TrackingType type)
        {
            try
            {
                if (!Enabled || TrackingId.IsNullOrEmpty() || type != TrackingType) return;

                var tracking = GetTracking(context);

                SendAnalysis(tracking);

            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Page Analyzer analyze method");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventTracking"></param>
        public override void Analyze(HttpContext context, EventTracking eventTracking)
        {            
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
            host = "http://dev.joyceprint.com";
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
            pageTracking.Append($"v={Version}");

            // Tracking Id / Property Id
            pageTracking.Append($"&tid={TrackingId}");

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