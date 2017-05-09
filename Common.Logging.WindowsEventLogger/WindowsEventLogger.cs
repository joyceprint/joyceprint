using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Web.Hosting;
using Common.Logging.Enums;

namespace Common.Logging.WindowsEventLogger
{
    public class WindowsEventLogger : LogProvider
    {
        private bool _enabled;

        private string _logName = "JoycePrint";

        private string _source = "JoycePrint Website";
        
        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            try
            {
                base.Initialize(providerName, providerConfig);

                if (providerConfig["enabled"] == null)
                    throw new Exception($"Error reading enabled configuration setting. Default of {_enabled} will be used for the provider {providerName}");

                _enabled = bool.Parse(providerConfig["enabled"]);

                if (providerConfig["log"] == null)
                    throw new Exception($"Error reading log configuration setting. Default of {_logName} will be used for the provider {providerName}");

                _logName = providerConfig["log"];

                if (providerConfig["source"] == null)
                    throw new Exception($"Error reading source configuration setting. Default of {_source} will be used for the provider {providerName}");

                _source = providerConfig["source"];

                if (_enabled && !EventLog.SourceExists(_source))
                    EventLog.CreateEventSource(_source, _logName);
            }
            catch (Exception logException)
            {
                EventLog.WriteEntry("Application", $"Error trying to create the '{_source}' in the eventlog. Make sure eventLog is created." + Environment.NewLine + "Exception info: " + logException.Message);
            }            
        }

        public override void Log(MessageLevel messageLevel, string message)
        {
            if (!_enabled) return;

            EventLog.WriteEntry(_source, message, ConvertToEvent(messageLevel));
        }

        public override void Log(MessageLevel messageLevel, Exception ex)
        {
            Log(messageLevel, ConvertToMessage(ex));
        }

        public override void Log(MessageLevel messageLevel, Exception ex, string additionalMessage)
        {
            var message = additionalMessage + Environment.NewLine + ConvertToMessage(ex);

            Log(messageLevel, message);
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