using System.Web.Mvc;
using Common.Analytics.Tracking;
using Common.Analytics;

namespace JoycePrint.Web.Attributes
{
    public class PageAnalysis : ActionFilterAttribute
    {
        public string Name;
        public string Host;
        public string Title;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var tracking = new Page
            {
                Name = Name,
                Host = Host ?? filterContext.HttpContext.Request.Url.AbsoluteUri,
                Title = Title                           
            };

            Analyzer.Instance.PageAnalysis(filterContext.HttpContext.ApplicationInstance.Context, tracking);
        }
    }
}