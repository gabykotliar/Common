using System;
using System.Diagnostics;
using Common;
using Common.Exceptions;
using Common.Logging;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Common.Exceptions
{
    [TestClass]
    public class When_handling_exceptions : BaseTest   
    {
        [TestInitialize]
        public void SetupTest()
        {
            SetAppServicesFactory();
        }

        [TestMethod]
        public void When_using_LogIfException_exception_is_handled()
        {
            Assert.Throws<DivideByZeroException>(LogIfExceptionMethod);
        }

        [TestMethod]
        public void When_using_HideException_exception_is_handled()
        {
            var zero = 0;

            ExceptionHandler.HideException(() =>
            {
                var x = 1 / zero;
            });
        }

        [TestMethod]
        public void When_using_IfException_exception_is_handled()
        {
            var zero = 0;

            ExceptionHandler.IfException(() =>
            {
                var x = 1 / zero;
            },
            DoSomethingWithException);


            ExceptionHandler.IfException(() =>
            {
                var x = 1 / zero;
            },
            ex => Debug.Write(ex.Message));

        }

        public void DoSomethingWithException(Exception ex)
        {

        }

        private void SetAppServicesFactory()
        {
            var logger = new Mock<Logger>();
            var factory = new Mock<Factory>();
            logger.Setup(x => x.GetLogEntry()).Returns(new LogEntry());
            factory.Setup(x => x.GetInstance<Logger>()).Returns(logger.Object);

            AppServices.SetFactory(factory.Object);
        }

        private void LogIfExceptionMethod()
        {
            var zero = 0;
           
            ExceptionHandler.LogIfException(() =>
            {
                var x = 1 / zero;
            });
        }
    }
}
