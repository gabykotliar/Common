using System;

using Common.Logging;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Common.Tests.Common.Logging
{
    [TestClass]
    public class LogTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        [Description("Message based log writes the right message.")]
        public void MessageBasedLogTest()
        {
            var entry = new LogEntry();
            var mockLogger = new Mock<Logger>();

            mockLogger.Setup(m => m.GetLogEntry()).Returns(entry);
            mockLogger.Setup(m => m.Write(entry));

            const string Message = "asdf";

            new Log(mockLogger.Object)
                .Message(Message)
                .Write();

            entry.Message.Should().Be(Message);
            mockLogger.VerifyAll();
        }

        [TestMethod]
        [Description("Exception based log writes the right exception, severity and message.")]
        public void ExceptionBasedLogTest()
        {
            var entry = new LogEntry();
            var mockLogger = new Mock<Logger>();

            mockLogger.Setup(m => m.GetLogEntry()).Returns(entry);
            mockLogger.Setup(m => m.Write(entry));

            var ex = new Exception("message");

            new Log(mockLogger.Object)
                .Exception(ex)
                .Write();

            entry.Message.Should().Be(ex.Message);
            entry.Exception.Should().Be(ex);
            entry.Severity.Should().Be(Severity.Error);
            
            mockLogger.VerifyAll();
        }

        [TestMethod]
        [Description("Fluent Message assignment overrides the original message")]
        public void FluentMessageAssignmentTest()
        {
            var entry = new LogEntry();
            var mockLogger = new Mock<Logger>();

            mockLogger.Setup(m => m.GetLogEntry()).Returns(entry);
            mockLogger.Setup(m => m.Write(entry));

            const string Message = "asdf";

            new Log(mockLogger.Object)
                .Exception(new Exception("original message"))
                .WithMessage(Message)
                .Write();

            entry.Message.Should().Be(Message);
            mockLogger.VerifyAll();
        }

        [TestMethod]
        [Description("Fluent severity assignment overrides the original severity")]
        public void FluentSeverityAssignmentTest()
        {
            var entry = new LogEntry();
            var mockLogger = new Mock<Logger>();

            mockLogger.Setup(m => m.GetLogEntry()).Returns(entry);
            mockLogger.Setup(m => m.Write(entry));

            new Log(mockLogger.Object)
                .Exception(new Exception("original message"))
                .With(Severity.Information)
                .Write();

            entry.Severity.Should().Be(Severity.Information);
            mockLogger.VerifyAll();
        }

        [TestMethod]        
        public void FluentExceptionAssignmentTest()
        {
            var entry = new LogEntry();
            var mockLogger = new Mock<Logger>();

            mockLogger.Setup(m => m.GetLogEntry()).Returns(entry);
            mockLogger.Setup(m => m.Write(entry));

            var ex = new Exception();

            new Log(mockLogger.Object)
                .Message(string.Empty)
                .WithException(ex)
                .Write();

            entry.Exception.Should().Be(ex);
            mockLogger.VerifyAll();
        }

        [TestMethod]
        public void FluentExceptionDoesNotOverrideSetMessageTest()
        {
            var entry = new LogEntry();
            var mockLogger = new Mock<Logger>();

            mockLogger.Setup(m => m.GetLogEntry()).Returns(entry);
            mockLogger.Setup(m => m.Write(entry));

            var ex = new Exception("asdf");
            var message = "original message";

            new Log(mockLogger.Object)
                .Message(message)
                .WithException(ex)
                .Write();

            entry.Message.Should().Be(message);
            mockLogger.VerifyAll();
        }
    }    
}
