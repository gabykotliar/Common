using System;

namespace Common.Logging
{
    public class LogEntry
    {
        public LogEntry()
        {
            Severity = Severity.Information;
        }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public Severity Severity { get; set; }
    }
}
