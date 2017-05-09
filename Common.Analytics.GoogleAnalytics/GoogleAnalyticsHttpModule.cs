using System;
using System.Web;
using Common.Analytics.Tracking;

namespace Common.Analytics.GoogleAnalytics
{
    public class GoogleAnalyticsHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        public void Dispose() { }

        private void OnBeginRequest(object source, EventArgs evernArgs)
        {            
            Analyzer.Instance.PageAnalysis(((HttpApplication)source).Context, GetPageTracking(((HttpApplication)source).Context));
        }

        private Page GetPageTracking(HttpContext context)
        {
            var page = context.Request.Url.AbsoluteUri;
            string sa;

            if (null != (sa = context.Request.Headers["soapaction"]))
            {
                var index = sa.LastIndexOf('/');
                page += sa.Substring((index == -1 ? 0 : index));
            }
            // TODO: What should the value for Host be ?
            var tracking = new Page
            {
                Host = context.Request.Url.AbsoluteUri, 
                Title = null,
                Name = page
            };

            return tracking;
        }
    }
}