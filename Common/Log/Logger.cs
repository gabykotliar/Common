namespace Common.Log
{
    public interface Logger
    {
        LogEntry GetLogEntry();

        void Write(LogEntry logEntry);
    }
}