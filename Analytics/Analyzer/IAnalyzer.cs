using System.Web;

namespace Analytics.Analyzer
{
    public interface IAnalyzer
    {
        void Analyze(HttpContext context, TrackingType type);
    }
}