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
        public CachingSubsection Caching
        {
            get
            {
                return (CachingSubsection)this["caching"];
            }
        }
    }
}
