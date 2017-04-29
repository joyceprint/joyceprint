using System;

namespace Common.Providers
{
    public class ProviderCollection<T> : System.Configuration.Provider.ProviderCollection where T : ProviderBase
    {
        public new T this[string name] => (T) base[name];

        public override void Add(System.Configuration.Provider.ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            if (!(provider is T))
            {
                var providerTypeName = typeof(T).ToString();
                throw new ArgumentException($"Provider must implement {providerTypeName} type");
            }

            base.Add(provider);
        }
    }
}