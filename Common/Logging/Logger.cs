namespace Common.Logging
{
    public interface Logger
    {
        LogEntry GetLogEntry();

        void Write(LogEntry logEntry);
    }
}