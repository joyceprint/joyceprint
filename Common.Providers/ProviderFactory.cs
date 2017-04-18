using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace Common.Providers
{
    public class ProviderFactory
    {
        /// <summary>
        /// Helper method for populating a provider collection from a Provider section handler abd setting a reference to a default provider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sectionName"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ProviderCollection<T> Initialize<T>(string sectionName, out T provider) where T : ProviderBase
        {
            ProviderCollection<T> providers;
            provider = null;

            // Get the reference to the provider section
            var configSection = (ProviderConfigurationSection) ConfigurationManager.GetSection(sectionName);

            if (configSection != null)
            {
                // Load the registered providers and point provider to the default provider
                providers = new ProviderCollection<T>();

                // Instantiate the providers and add them to the ProviderCollection as providers of type T
                ProvidersHelper.InstantiateProviders(configSection.Providers, providers, typeof(T));

                // Set a reference to the default provider
                provider = providers[configSection.DefaultProvider];

                if (provider == null) throw new ProviderException($"Unalbe to load default '{sectionName}' provider");
            }
            else
            {
                throw new ProviderException($"Provider configuration section '{sectionName}' missing.");
            }

            return providers;
        }

        /// <summary>
        /// Helper method for populating a provider collection from a Provider section handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static ProviderCollection<T> InstantiateProviders<T>(string sectionName) where T : ProviderBase
        {
            ProviderCollection<T> providers;
            var configSection = (ProviderConfigurationSection) ConfigurationManager.GetSection(sectionName);

            if (configSection != null)
            {
                providers = new ProviderCollection<T>();
                ProvidersHelper.InstantiateProviders(configSection.Providers, providers, typeof(T));
            }
            else
            {
                throw new ProviderException($"Provider configuration section '{sectionName}' missing.");
            }

            return providers;
        }

        /// <summary>
        /// Helper method of instantiating a specific provider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sectionName"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public static T InstantiateProvider<T>(string sectionName, string providerName) where T : ProviderBase
        {
            T provider;

            var configSection = (ProviderConfigurationSection) ConfigurationManager.GetSection(sectionName);

            if (configSection != null)
            {
                var providerSettings = configSection.Providers[providerName];
                if (providerSettings == null) throw new ProviderException($"Unable to find '{sectionName}' provider");

                provider = ProvidersHelper.InstantiateProvider(providerSettings, typeof(T)) as T;
                if (provider == null)
                    throw new ProviderException($"Provider configuration section '{sectionName}' missing.");
            }
            else
            {
                throw new ProviderException($"Provider configuration section '{sectionName}' missing.");
            }

            return provider;
        }

        /// <summary>
        /// Instantiate the default provider for the providers collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static T InstantiateDefaultProvider<T>(string sectionName) where T : ProviderBase
        {
            T provider;

            try
            {
                var configSection = (ProviderConfigurationSection) ConfigurationManager.GetSection(sectionName);
                if (configSection != null)
                {
                    var providerSettings = configSection.Providers[configSection.DefaultProvider];
                    if (providerSettings == null) throw new ProviderException($"Unable to find default '{sectionName}' provider");

                    provider = ProvidersHelper.InstantiateProvider(providerSettings, typeof(T)) as T;
                    if (provider == null) throw new ProviderException($"Unable to load default'{sectionName}' provider");
                }
                else
                {
                    throw new ProviderException($"Provider configuration section '{sectionName}' missing.");
                }
            }
            catch (ProviderException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProviderException($"Unable to load default '{sectionName}' provider. Error: {ex.Message}");
            }

            return provider;
        }
    }
}