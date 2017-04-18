using System;
using JoycePrint.Domain.Enums;

namespace JoycePrint.Domain.Log
{
    public class EventLogger : LogBase
    {
        public override void Log(string message, MessageLevel level)
        {
            // update this to write to the event log so this can be tested
            // later it can write to the database

            // https://csharp.today/log4net-tutorial-great-library-for-logging/
            // http://www.infoworld.com/article/2980677/application-architecture/implement-a-simple-logger-in-c.html
            throw new NotImplementedException();
        }
    }
}
