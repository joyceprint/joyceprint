using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Common.Providers
{
    public abstract class ProviderBase : System.Configuration.Provider.ProviderBase
    {
        [Description("Allows the config file entries to be updated without requiring a restart")]        
        public bool EnableUpdates { get; protected set; }

        /// <summary>
        /// Initializes providers from the provider section of the config file
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerConfig"></param>
        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            // Check the configuration parameters
            if (providerConfig == null) throw new ArgumentNullException(nameof(providerConfig));

            // Check the provider name, if it does not exist set it to the name of the type
            if (string.IsNullOrEmpty(providerName)) providerName = GetType().ToString();

            // Set the provider description, this will come from the config file.
            // If it's blank it will be reset to the type of this provider
            if (string.IsNullOrEmpty(providerConfig["description"]))
            {
                providerConfig.Remove("description");
                providerConfig.Add("description", GetType().ToString());
            }

            // Set whether or not updates are enabled depending on the provider you may want to 
            // ignore what is in the config file or not.
            if (string.IsNullOrEmpty(providerConfig["enableUpdates"])) EnableUpdates = false;
            else EnableUpdates = bool.Parse(providerConfig["enableUpdates"]);
            
            base.Initialize(providerName, providerConfig);
        }
    }
}