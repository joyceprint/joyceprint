using System.ComponentModel;
using System.Configuration;

namespace Common.Providers
{
    public class ProviderConfigurationSection : ConfigurationSection
    {
        [Description("The default provider to use for the providers group")]
        private readonly ConfigurationProperty _defaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), null);

        [Description("The providers section for the providers group")]
        private readonly ConfigurationProperty _providers = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null);

        [Description("The configuration properties for the provider, this is in the form of an ARC [Add, Remove, Clear] map")]
        private readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        public ProviderConfigurationSection()
        {
            _properties.Add(_providers);
            _properties.Add(_defaultProvider);
        }

        [ConfigurationProperty("defaultProvider", IsRequired = true)]
        public string DefaultProvider
        {
            get { return (string) base[_defaultProvider]; }
            set { base[_defaultProvider] = value; }
        }

        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection) base[_providers]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }
    }
}