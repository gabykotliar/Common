using Common.Caching;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.Cache
{
    [TestClass]
    public class MemoryCacheAdapterTests
    {
        [TestMethod]
        public void ConcreteTypeIsConcreteTest()
        {
            var adapter = new MemoryCacheAdapter();
            var value = new object();

            adapter.Add("asdf", value);

            adapter.Get<object>("asdf").Should().Be(value);
        }        
    }    
}
