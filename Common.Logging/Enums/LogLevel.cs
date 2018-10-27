using System;
using System.ComponentModel;

namespace Common.Logging.Enums
{    
    [Description("Enum indicating the level of logging to be turned on")]
    [Flags]
    public enum LogLevel
    {
        Off,
        Information,
        Security,
        Warning,
        Error,
        Fatal,
        Debug,
        All
    }
}