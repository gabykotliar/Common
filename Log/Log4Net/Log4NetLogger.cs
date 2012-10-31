using log4net;

namespace Common.Log.Log4Net
{
    internal class Log4NetLogger : Logger
    {
        private readonly ILog logger;

        public Log4NetLogger()
        {
            this.logger = LogManager.GetLogger(this.GetType());
        }

        public LogEntry GetLogEntry()
        {
            return new LogEntry();
        }

        public void Write(LogEntry logEntry)
        {
            switch (logEntry.Severity)
            {
                case Severity.Critical: 
                    if (logEntry.Exception != null)
                        this.logger.Fatal(logEntry.Message, logEntry.Exception);
                    else
                        this.logger.Fatal(logEntry.Message);
                    break;
                case Severity.Error:
                    if (logEntry.Exception != null)
                        this.logger.Error(logEntry.Message, logEntry.Exception);
                    else
                        this.logger.Error(logEntry.Message);
                    break;
                case Severity.Warning:
                    if (logEntry.Exception != null)
                        this.logger.Warn(logEntry.Message, logEntry.Exception);
                    else
                        this.logger.Warn(logEntry.Message);
                    break;
                case Severity.Information:
                    if (logEntry.Exception != null)
                        this.logger.Info(logEntry.Message, logEntry.Exception);
                    else
                        this.logger.Info(logEntry.Message);
                    break;
                case Severity.Verbose:
                    if (logEntry.Exception != null)
                        this.logger.Debug(logEntry.Message, logEntry.Exception);
                    else
                        this.logger.Debug(logEntry.Message);
                    break;
            }
        }
    }
}