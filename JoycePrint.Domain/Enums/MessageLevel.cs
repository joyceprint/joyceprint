using System.Diagnostics.CodeAnalysis;

namespace JoycePrint.Domain.Enums
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum MessageLevel
    {
        INFORMATION,
        SECURITY,
        WARNING,
        ERROR,
        FATAL,
        DEBUG
    }
}