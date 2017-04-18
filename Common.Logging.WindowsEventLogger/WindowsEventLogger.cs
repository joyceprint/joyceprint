using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Common.Logging.Enums;

namespace Common.Logging.WindowsEventLogger
{
    public class WindowsEventLogger : LogProvider
    {
        private const string LogName = "JoycePrint";
        private const string Source = "JoycePrint Website";

        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            try
            {
                if (!EventLog.SourceExists(Source)) EventLog.CreateEventSource(Source, LogName);
            }
            catch (Exception logException)
            {
                EventLog.WriteEntry("Application", $"Error trying to create the '{Source}' in the eventlog. Make sure eventLog is created." + Environment.NewLine + "Exception info: " + logException.Message);
            }

            base.Initialize(providerName, providerConfig);
        }

        public override void Log(MessageLevel messageLevel, string message)
        {
            throw new NotImplementedException("implement this later");
        }
    }
}