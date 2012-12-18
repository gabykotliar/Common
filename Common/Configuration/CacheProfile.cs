using System.Configuration;

namespace Common.Configuration
{
    public class CacheProfile : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
        }

        [ConfigurationProperty("slidingExpiration", IsRequired = false)]
        public int SlidingExpiration
        {
            get
            {
                return (int)this["slidingExpiration"];
            }
        }
    }
}