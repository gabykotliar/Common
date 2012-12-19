using System;
using System.Configuration;
using Common;
using Common.Caching;
using Common.Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Common.Caching
{
    [TestClass]
    public class GenericCacheTests
    {
        [TestInitialize]
        public void CleanAppServicesStatics()
        {
            AppServices.SetCache(null);
            AppServices.SetFactory(null);
        }

        #region AppServices cachre related 

        [TestMethod]
        [Description("The AppServices cache property returns DisabledCache object if the cache is disabled in the configuration")]
        public void DisabledConfigurationFactoryTest()
        {
            var disabled = AppServicesConfiguration.Instance.Caching.Disabled;

            AppServicesConfiguration.Instance.Caching.Disabled = true;

            AppServices.SetFactory(new Mock<Factory>().Object);

            AppServices.Cache.Should().BeOfType<DisabledCache>();

            // leave the environment unchanged
            AppServicesConfiguration.Instance.Caching.Disabled = disabled;
        }

        [TestMethod]
        [Description("The AppServices cache property returns calls the provided factory to get the cache.")]
        public void CacheFactoryTest()
        {
            var mockFactory = new Mock<Factory>();
            var cache = new Mock<Cache>().Object;

            mockFactory.Setup(m => m.GetInstance<Cache>()).Returns(cache);

            AppServices.SetFactory(mockFactory.Object);

            var c = AppServices.Cache;
            c = AppServices.Cache; // call twice but factory should be called only once.

            mockFactory.Verify(m => m.GetInstance<Cache>(), Times.Once());
        }

        [TestMethod]
        [Description("The AppServices cache property returns the same object specified in SetCache.")]
        public void SetCacheTest()
        {
            var cache = new Mock<Cache>().Object;

            AppServices.SetCache(cache);

            AppServices.Cache.Should().BeSameAs(cache);
        }

        [TestMethod]
        [Description("The AppServices cache property throws an exception if it has no setted cache and no factory.")]
        public void GetCacheExceptionWithoutProvidersTest()
        {
            Cache c;
            Action act = () => c = AppServices.Cache;
            act.ShouldThrow<ConfigurationErrorsException>();
        }

        #endregion

        #region Cache class

        [TestMethod]
        public void CacheTest()
        {
            var mockCache = new Mock<Cache>();
            var obj = new { Prop = "asdf" };
            var key = "obj";

            mockCache.CallBase = true;
            mockCache.Setup(m => m.Add(key, obj, It.IsAny<TimeSpan>()));

            mockCache.Object.Add(key, obj, "myDefault");

            mockCache.Verify(m => m.Add(key, obj, It.Is<TimeSpan>(ts => ts.TotalSeconds.Equals(60))));
        }

        #endregion
    }
}
