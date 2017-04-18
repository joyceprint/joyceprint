using System.ComponentModel;

namespace Common.Logging.Enums
{   
    [Description("Enum indicating the type of message being logged")]
    public enum MessageLevel
    {
        Information,
        Security,
        Warning,
        Error,
        Fatal,
        Debug
    }
}