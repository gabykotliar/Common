using System.Configuration;

namespace Common.Configuration
{
    public class CachingSubsection : ConfigurationElement
    {
        public override bool IsReadOnly()
        {
            // this will allow to override configuration settings during test.
            return false;
        }

        [ConfigurationProperty("disabled", DefaultValue = false, IsRequired = false)]
        public bool Disabled
        {
            get { return (bool)this["disabled"]; }
            set { this["disabled"] = value; }
        }

        [ConfigurationProperty("profiles", Options = ConfigurationPropertyOptions.IsRequired)]
        public CachingProfilesCollection Profiles
        {
            get { return (CachingProfilesCollection)this["profiles"]; }
        }
    }
}
