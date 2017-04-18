using JoycePrint.Domain.Enums;

namespace JoycePrint.Domain.Log
{
    public abstract class LogBase
    {
        public abstract void Log(string message, MessageLevel level);
    }
}