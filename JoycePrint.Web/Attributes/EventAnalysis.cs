using System.Web.Mvc;
using Analytics;
using Analytics.Enums;

namespace JoycePrint.Web.Attributes
{
    public class EventAnalysis : ActionFilterAttribute
    {
        public string Category;
        public string Action;
        public string Label;
        public string Value;
        public TrackingType TrackingType;
            
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var eventTracking = new EventTracking
            {
                Category = Category,
                Action = Action,
                Label = Label,
                Value = Value,
                TrackingType = TrackingType
            };

            base.OnActionExecuting(filterContext);

            // If this is a child action we want to skip event analysis
            if (filterContext.IsChildAction) return;

            Analytics.Analytics.Engine.CaptureEventAnalysis(filterContext.HttpContext.ApplicationInstance.Context, eventTracking);
        }
    }
}