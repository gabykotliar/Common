using System.Configuration;

namespace Common.Configuration
{
    public class AppServicesConfiguration : ConfigurationSection
    {
        public static AppServicesConfiguration Instance 
        { 
            get
            {
                return (AppServicesConfiguration)ConfigurationManager.GetSection("applicationServices");
            }
        }

        [ConfigurationProperty("caching", IsRequired = false)]
        public CachingSubSection Caching
        {
            get
            {
                return (CachingSubSection)this["caching"];
            }
        }
    }
}
