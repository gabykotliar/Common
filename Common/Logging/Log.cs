using System;

namespace Common.Logging
{
    public class Log
    {
        #region Attributes

        private readonly Logger logger;
        private LogEntry entry;

        #endregion

        public Log(Logger logger)
        {
            this.logger = logger;            
        }

        private Log Build()
        {
            entry = logger.GetLogEntry();

            return this;
        }

        public Log Message(string text)
        {
            return Build().WithMessage(text);
        }

        public Log Exception(Exception ex)
        {
            return Build()
                    .WithException(ex)
                    .WithMessage(ex.Message)
                    .With(Severity.Error);
        }

        public Log WithMessage(string message)
        {
            entry.Message = message;
            return this;
        }

        public Log With(Severity severity)
        {
            entry.Severity = severity;
            return this;
        }

        public Log WithException(Exception ex)
        {
            entry.Exception = ex;
            return this;
        }

        public void Write()
        {
            logger.Write(this.entry);
        }
    }
}
