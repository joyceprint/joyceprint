using System.Configuration;

namespace Analytics.Analyzer
{
    /// <summary>
    /// Read the web configuration element to get a list of analytics providers
    /// </summary>
    public class AnalysisEngineConfigurationSection : ConfigurationSection
    {        
        private readonly ConfigurationProperty _enabled = new ConfigurationProperty("enabled", typeof(bool), false);

        private readonly ConfigurationProperty _analysers = new ConfigurationProperty("analyzers",
            typeof(ProviderSettingsCollection), null);        

        private ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        [ConfigurationProperty("enabled", IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)base[_enabled]; }
            set { base[_enabled] = value; }
        }

        [ConfigurationProperty("analyzers", IsRequired = true)]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base[_analysers]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }

        public AnalysisEngineConfigurationSection()
        {
            _properties.Add(_enabled);
            _properties.Add(_analysers);
        }
    }
}
