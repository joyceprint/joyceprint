using System;
using System.Collections.Specialized;
using Common.Logging.Enums;
using Elmah;

namespace Common.Logging.ElmahLogger
{
    public class ElmahLogger : LogProvider
    {
        private bool _enabled;

        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            try
            {
                base.Initialize(providerName, providerConfig);

                if (providerConfig["enabled"] == null)
                    throw new Exception($"Error reading enabled configuration setting. Default of {_enabled} will be used for the provider {providerName}");

                _enabled = bool.Parse(providerConfig["enabled"]);
            }
            catch (Exception logException)
            {
                ErrorSignal.FromCurrentContext().Raise(logException);
            }
        }

        private void Log(Exception ex)
        {
            if (!_enabled) return;

            try { ErrorSignal.FromCurrentContext().Raise(ex); }
            catch { }
        }

        public override void Log(MessageLevel messageLevel, string message)
        {
            Log(new Exception(message));
        }

        public override void Log(MessageLevel messageLevel, Exception ex)
        {
            Log(ex);
        }

        public override void Log(MessageLevel messageLevel, Exception ex, string additionalMessage)
        {
            ex.Data.Add("Additional Message", additionalMessage);
            Log(ex);
        }
    }
}