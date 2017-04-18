using System;
using System.Diagnostics.CodeAnalysis;

namespace JoycePrint.Domain.Enums
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [Flags]
    public enum LogLevel
    {
        OFF,
        INFORMATION,
        SECURITY,
        WARNING,
        ERROR,
        FATAL,
        DEBUG,
        ALL
    }
}