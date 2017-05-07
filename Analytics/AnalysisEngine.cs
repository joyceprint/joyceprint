using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Analytics.Analyzer;
using Analytics.Enums;

// ReSharper disable CoVariantArrayConversion
namespace Analytics
{
    public class AnalysisEngine
    {
        /// <summary>
        /// This is the enabled switch for the analytics section <engine enabled="true" />
        /// </summary>
        private bool Enabled { get; set; }

        private AnalyzerProvider[] Analyzers { get; set; }

        /// <summary>
        /// The application name that will be used when logging
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static string _applicationName = HostingEnvironment.SiteName;

        public AnalysisEngine()
        {
            var analyzers = AnalyzerProviderFactory.Analyzers;
            Analyzers = new AnalyzerProvider[analyzers.Count];
            analyzers.CopyTo(Analyzers, 0);

            var section = (AnalysisEngineConfigurationSection) ConfigurationManager.GetSection(Config.EngineConfigSectionName);

            Enabled = null != section && section.Enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        public void CaptureAnalysis(HttpContext context, TrackingType type)
        {
            if (!Enabled || null == Analyzers) return;

            foreach(var analyzer in Analyzers.Where(e => Enabled))
                analyzer.Analyze(context, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventTracking"></param>
        public void CaptureEventAnalysis(HttpContext context, EventTracking eventTracking)
        {
            if (!Enabled || null == Analyzers) return;

            foreach (var analyzer in Analyzers.Where(e => Enabled))
                analyzer.Analyze(context, eventTracking);
        }
    }
}