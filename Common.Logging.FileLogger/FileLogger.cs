using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text;
using Common.Logging.Enums;

namespace Common.Logging.FileLogger
{
    public class FileLogger : LogProvider
    {
        private bool _enabled;

        private string _logFile = $"Log_{DateTime.Today}";

        private string _logPath = @"D:\Websites\Logs";
        
        public override void Initialize(string providerName, NameValueCollection providerConfig)
        {
            try
            {
                base.Initialize(providerName, providerConfig);

                if (providerConfig["enabled"] == null)
                    throw new Exception($"Error reading enabled configuration setting. Default of {_enabled} will be used for the provider {providerName}");

                _enabled = bool.Parse(providerConfig["enabled"]);

                if (providerConfig["logFile"] == null)
                    throw new Exception($"Error reading log configuration setting. Default of {_logFile} will be used for the provider {providerName}");

                _logFile = providerConfig["logFile"];

                if (providerConfig["logPath"] == null)
                    throw new Exception($"Error reading source configuration setting. Default of {_logPath} will be used for the provider {providerName}");

                _logPath = providerConfig["logPath"];
            }
            catch (Exception logException)
            {
                EventLog.WriteEntry("Application", $"Error trying to create the file '{_logPath}{_logFile}'." + Environment.NewLine + "Exception info: " + logException.Message);
            }
        }

        public override void Log(MessageLevel messageLevel, string message)
        {
            if (!_enabled) return;

            var seperator = "-----------------------------------------------------------------------------------------------------";
            using (var fs = new FileStream(Path.Combine(_logPath, _logFile), FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"{DateTime.Now} :: {messageLevel} {Environment.NewLine} {message} {Environment.NewLine} {seperator}");
                }
            }
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