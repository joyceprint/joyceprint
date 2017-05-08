using System.Collections.Specialized;
using System.Web;
using System.Web.Hosting;
using Common.Analytics.Tracking;

namespace Common.Analytics
{
    public abstract class AnalyticsProvider : Providers.ProviderBase
    {
        protected static readonly string ApplicationName = HostingEnvironment.SiteName;

        // ReSharper disable once RedundantOverriddenMember
        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            base.Initialize(providerName, providerConfig);
        }

        public abstract void PageAnalysis(HttpContext context, Page tracking);

        public abstract void EventAnalysis(HttpContext context, Event tracking);
    }
}