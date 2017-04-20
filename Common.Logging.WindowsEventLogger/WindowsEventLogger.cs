using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
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
            EventLog.WriteEntry(Source, message, ConvertToEvent(messageLevel));
        }

        public override void Log(MessageLevel messageLevel, Exception ex)
        {
            Log(messageLevel, ConvertToMessage(ex));
        }

        private string ConvertToMessage(Exception ex)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Exception Message : {ex.Message}");
            sb.AppendLine($"Stack Trace : {ex.StackTrace}");
            sb.AppendLine($"Inner Exception : {ex.InnerException}");

            return sb.ToString();
        }

        private EventLogEntryType ConvertToEvent(MessageLevel level)
        {
            switch (level)
            {
                case MessageLevel.Information: return EventLogEntryType.Information;
                case MessageLevel.Security: return EventLogEntryType.SuccessAudit;
                case MessageLevel.Warning: return EventLogEntryType.Warning;
                case MessageLevel.Error: return EventLogEntryType.Error;
                case MessageLevel.Fatal: return EventLogEntryType.Error;
                case MessageLevel.Debug: return EventLogEntryType.Information;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}