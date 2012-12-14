using Common.Caching;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.Caching
{
    [TestClass]
    public class MemoryCacheAdapterTests
    {
        [TestMethod]
        public void AddGenericTest()
        {
            var adapter = new MemoryCacheAdapter();
            var value = new object();

            adapter.Add("asdf", value);

            adapter.Get<object>("asdf").Should().Be(value);
        }
    }    
}
