using System.Web.Mvc;
using Analytics;
using Analytics.Enums;

namespace JoycePrint.Web.Attributes
{
    public class EventAnalysis : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var eventTracking = new EventTracking
            {
                Category = "User Interaction",
                Action = "Quote",
                Label = "Quote Request",
                Value = "0",
                TrackingType = TrackingType.Event
            };

            base.OnActionExecuting(filterContext);

            // If this is a child action we want to skip event analysis
            if (filterContext.IsChildAction) return;

            Analytics.Analytics.Engine.CaptureEventAnalysis(filterContext.HttpContext.ApplicationInstance.Context, eventTracking);
        }
    }
}