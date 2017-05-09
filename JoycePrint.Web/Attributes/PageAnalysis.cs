using System;
using System.Web.Mvc;
using Common.Analytics.Tracking;
using Common.Analytics;
using Common.Logging;
using Common.Logging.Enums;

namespace JoycePrint.Web.Attributes
{
    public class PageAnalysis : ActionFilterAttribute
    {
        public string Name;        
        public string Title;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                var tracking = new Page
                {
                    Host = filterContext.HttpContext.Request.Url?.AbsoluteUri,
                    Name = Name,                    
                    Title = Title
                };

                Analyzer.Instance.PageAnalysis(filterContext.HttpContext.ApplicationInstance.Context, tracking);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, $"Error running page analysis");
            }
        }
    }
}