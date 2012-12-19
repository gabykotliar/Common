using Common;
using Common.Caching;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Aspects
{
    [TestClass]
    public class CacheAspectTest
    {
        [TestMethod]
        public void AspectReturnFromCacheTest()
        {
            AppServices.SetCache(new MemoryCache());

            var obj = new CachedClass();

            var value = obj.Get();

            Assert.AreEqual(value, obj.Get());

            AppServices.Cache.InvalidateCacheItem("Test.Aspects.CachedClass.Get?");

            Assert.AreNotEqual(value, obj.Get());
        }
    }
}
