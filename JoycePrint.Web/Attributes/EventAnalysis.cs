﻿using System;
using System.Web.Mvc;
using Common.Analytics.Tracking;
using Common.Analytics;
using Common.Logging;
using Common.Logging.Enums;

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

            try
            {
                var tracking = new Event
                {
                    Category = Category,
                    Action = Action,
                    Label = Label,
                    Value = Value
                };

                Analyzer.Instance.EventAnalysis(filterContext.HttpContext.ApplicationInstance.Context, tracking);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, $"Error running event analysis");
            }
        }
    }
}