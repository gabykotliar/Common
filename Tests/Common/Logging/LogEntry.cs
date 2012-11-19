using Common.Logging;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.Common.Logging
{
    [TestClass]
    public class LogEntryTests
    {
        [TestMethod]
        public void DefaultSeverityIsInformation()
        {
            var entry = new LogEntry();

            entry.Severity.Should().Be(Severity.Information);
        }        
    }    
}
