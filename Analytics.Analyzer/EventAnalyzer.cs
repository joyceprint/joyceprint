using Common.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web;
using Analytics.Enums;
using Common.Logging.Enums;

namespace Analytics.Analyzer
{
    public class EventAnalyzer : Analyzer
    {
        public override void Analyze(HttpContext context, TrackingType type)
        {
        }

        public override void Analyze(HttpContext context, EventTracking eventTracking)
        {
            try
            {
                if (!Enabled || TrackingId.IsNullOrEmpty() || eventTracking.TrackingType != TrackingType) return;

                var tracking = GetTracking(context, eventTracking);

                SendAnalysis(tracking);

            }
            catch (Exception ex)
            {
                Logger.Instance.Log(MessageLevel.Error, ex, "Event Analyzer analyze method");
            }
        }

        private string GetTracking(HttpContext context, EventTracking eventTracking)
        {
            var tracking = new StringBuilder();

            // Version
            tracking.Append($"v={Version}");

            // Tracking Id / Property Id
            tracking.Append($"&tid={TrackingId}");

            // Anonymous Client Id
            tracking.Append($"&cid={(context.Request.UserHostAddress.IsNullOrEmpty() ? "unknown" : context.Request.UserHostAddress)}");

            // Hit Type [ Type is Event ]
            tracking.Append($"&t=event");

            // Event Category [ Required ]
            tracking.Append($"&ec={eventTracking.Category}");

            // Event Action [ Required ]
            tracking.Append($"&ea={eventTracking.Action}");

            // Event Label
            tracking.Append($"&el={eventTracking.Label}");

            // Event Value [ Monetary value associated with the event ]
            tracking.Append($"&ev={eventTracking.Value}");

            return tracking.ToString();
        }
    }
}