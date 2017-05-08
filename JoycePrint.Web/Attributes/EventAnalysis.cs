using System.Web.Mvc;
using Common.Analytics.Tracking;
using Common.Analytics;

namespace JoycePrint.Web.Attributes
{
    public class EventAnalysis : ActionFilterAttribute
    {
        public string Category;
        public string Action;
        public string Label;
        public string Value;        
            
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var tracking = new Event
            {
                Category = Category,
                Action = Action,
                Label = Label,
                Value = Value
            };
            
            Analyzer.Instance.EventAnalysis(filterContext.HttpContext.ApplicationInstance.Context, tracking);
        }
    }
}