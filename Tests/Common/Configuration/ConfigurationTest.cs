using System.Configuration;
using Common.Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.Configuration
{
    [TestClass]
    public class ConfigurationTest
    {
        private AppServicesConfiguration configuration;

        [TestInitialize]
        public void LoadSection()
        {
            configuration = (AppServicesConfiguration)ConfigurationManager.GetSection("applicationServices");
        }

        [TestMethod]
        public void GetAppServicesSectionTest()
        {
            configuration.Should().NotBeNull();
        }

        [TestMethod]
        public void CachingSectionTest()
        {
            configuration.Caching.Should().NotBeNull();
            configuration.Caching.Disabled.Should().BeFalse();
            
            var profiles = configuration.Caching.Profiles;
            profiles.Should().HaveCount(2);
            profiles.Default.Should().Be("myDefault");
            profiles["myDefault"].SlidingExpiration.Should().Be(60);
            profiles[1].SlidingExpiration.Should().Be(1);
        }
    }
}
