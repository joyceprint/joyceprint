using System.Web;
using Analytics.Enums;

namespace Analytics.Analyzer
{
    public interface IAnalyzer
    {
        void Analyze(HttpContext context, TrackingType type);

        void Analyze(HttpContext context, EventTracking eventTracking);
    }
}