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
            // TODO: we need to map our messge levels to the event log
            // Add this as an abstract method of the base class
            // Ensure we have the correct amount of message levels
            // Could add Verbose???? or some others?
            EventLog.WriteEntry(Source, message, EventLogEntryType.Error);
        }
    }
}