using Common.Logging;

using log4net;

namespace Common.Log.Log4Net
{
    public class Log4NetLoggerAdapter : Logger
    {
        private readonly ILog logger;

        public Log4NetLoggerAdapter()
        {
            logger = LogManager.GetLogger(GetType());
        }

        internal Log4NetLoggerAdapter(ILog logger)
        {
            this.logger = logger;
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
                        logger.Fatal(logEntry.Message, logEntry.Exception);
                    else
                        logger.Fatal(logEntry.Message);
                    break;
                case Severity.Error:
                    if (logEntry.Exception != null)
                        logger.Error(logEntry.Message, logEntry.Exception);
                    else
                        logger.Error(logEntry.Message);
                    break;
                case Severity.Warning:
                    if (logEntry.Exception != null)
                        logger.Warn(logEntry.Message, logEntry.Exception);
                    else
                        logger.Warn(logEntry.Message);
                    break;
                case Severity.Information:
                    if (logEntry.Exception != null)
                        logger.Info(logEntry.Message, logEntry.Exception);
                    else
                        logger.Info(logEntry.Message);
                    break;
                case Severity.Verbose:
                    if (logEntry.Exception != null)
                        logger.Debug(logEntry.Message, logEntry.Exception);
                    else
                        logger.Debug(logEntry.Message);
                    break;
            }
        }
    }
}