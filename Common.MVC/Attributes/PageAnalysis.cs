using System;
using System.Web.Mvc;
using Common.Analytics;
using Common.Analytics.Tracking;
using Common.Logging;
using Common.Logging.Enums;

namespace Common.MVC.Attributes
{
    [Obsolete("This functionality has been moved into Google Tag Manager")]
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
                    Host = filterContext.HttpContext.Request.Url?.Host,
                    Name = GetPage(filterContext),                    
                    Title = Title
                };

                Analyzer.Instance.PageAnalysis(filterContext.HttpContext.ApplicationInstance.Context, tracking);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, $"Error running page analysis");
            }
        }

        private string GetPage(ControllerContext controllerContext)
        {
            var page = controllerContext.HttpContext.Request.Url?.AbsoluteUri;
            string sa;

            if (null != (sa = controllerContext.HttpContext.Request.Headers["soapaction"]))
            {
                var index = sa.LastIndexOf('/');
                page += sa.Substring((index == -1 ? 0 : index));
            }

            return page;
        }
    }
}