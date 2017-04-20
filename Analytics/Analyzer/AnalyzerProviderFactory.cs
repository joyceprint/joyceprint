using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace Analytics.Analyzer
{
    public static class AnalyzerProviderFactory
    {
        private static readonly object MyLock = new object();

        private static bool _isInitialized;

        private static ProviderCollection _analyzers;

        public static ProviderCollection Analyzers
        {
            get
            {
                if (!_isInitialized) Initialize();

                return _analyzers;
            }
        }

        private static void Initialize()
        {
            lock (MyLock)
            {
                if (!_isInitialized)
                {
                    InstantiateProviders(Config.EngineConfigSectionName);
                }
            }
        }

        private static void InstantiateProviders(string sectionName)
        {
            _analyzers = new ProviderCollection();
            var configSection = (AnalysisEngineConfigurationSection) ConfigurationManager.GetSection(sectionName);

            if (null != configSection)
            {
                ProvidersHelper.InstantiateProviders(configSection.Providers, _analyzers, typeof(AnalyzerProvider));

                if (null == _analyzers || _analyzers.Count == 0)
                    throw new ProviderException($"Unable to load {sectionName} analyzers");
            }
            else
            {
                throw new ProviderException($"Provider configuration section {sectionName} is missing.");
            }

            _isInitialized = true;
        }
    }
}