using Common.Caching;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.Caching
{
    [TestClass]
    public class MemoryCacheTests
    {
        [TestMethod]
        public void AddGenericTest()
        {
            var adapter = new MemoryCache();
            var value = new { Some = "string" };

            adapter.Add("asdf", value);

            adapter.Get<object>("asdf").Should().Be(value);
        }
    }    
}
