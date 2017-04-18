using System.Collections.Specialized;
using Common.Logging.Enums;

namespace Common.Logging
{
    public abstract class LogProvider : Providers.ProviderBase
    {
        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            base.Initialize(providerName, providerConfig);
        }

        public abstract void Log(MessageLevel messageLevel, string message);
    }
}