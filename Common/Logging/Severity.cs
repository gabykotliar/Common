namespace Common.Logging
{
    public enum Severity
    {
        /// <summary>
        /// Fatal error or application crash.
        /// </summary>
        Critical,

        /// <summary>
        /// Recoverable error.
        /// </summary>
        Error,

        /// <summary>
        /// Noncritical problem.
        /// </summary>
        Warning,

        /// <summary>
        /// Informational message.
        /// </summary>
        Information,

        /// <summary>
        /// Debugging trace.
        /// </summary>
        Verbose
    }
}
