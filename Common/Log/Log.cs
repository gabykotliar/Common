using System;

namespace Common.Log
{
    public class Log
    {
        #region Attributes

        private Logger logger;
        private LogEntry entry;

        #endregion

        private static Log Build()
        {
            var logger = Configuration.Factory.GetInstance<Logger>();

            return new Log(logger);
        }

        public static Log Message(string text)
        {
            return Build().WithMessage(text);
        }

        public static Log Exception(Exception ex)
        {
            return Build()
                    .WithException(ex)
                    .WithMessage(ex.Message)
                    .With(Severity.Error);
        }

        private Log(Logger logger)
        {
            this.logger = logger;
            this.entry = logger.GetLogEntry();
        }
       
        public Log WithMessage(string message)
        {
            this.entry.Message = message;
            return this;
        }

        public Log With(Severity severity)
        {
            this.entry.Severity = severity;
            return this;
        }

        public Log WithException(Exception ex)
        {
            this.entry.Exception = ex;
            return this;
        }

        public void Write()
        {
            this.logger.Write(this.entry);
        }
    }
}
