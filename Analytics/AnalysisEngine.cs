using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Analytics.Analyzer;

namespace Analytics
{
    public class AnalysisEngine
    {
        private bool Enabled { get; set; }

        private AnalyzerProvider[] Analyzers { get; set; }

        private static string _applicationName = HostingEnvironment.SiteName;

        public AnalysisEngine()
        {
            var analyzers = AnalyzerProviderFactory.Analyzers;
            Analyzers = new AnalyzerProvider[analyzers.Count];
            analyzers.CopyTo(Analyzers, 0);

            var section =
                (AnalysisEngineConfigurationSection) ConfigurationManager.GetSection(Config.EngineConfigSectionName);

            Enabled = null == section ? false : section.Enabled;
        }

        public void CaptureAnalysis(HttpContext context)
        {
            if (Enabled && null != Analyzers)
            {
                foreach(var analyzer in Analyzers.Where(e => Enabled))
                    analyzer.Analyze(context);
            }
        }
    }
}
