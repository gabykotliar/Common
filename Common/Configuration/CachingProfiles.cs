using System;
using System.Configuration;

namespace Common.Configuration
{
    [ConfigurationCollection(typeof(CacheProfile), AddItemName = "profile", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class CachingProfiles : ConfigurationElementCollection
    {
        #region overrided methods

        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheProfile();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((CacheProfile)element).Name;
        }

        #endregion

        #region Indexed access

        public CacheProfile this[int index]
        {
            get { return (CacheProfile)BaseGet(index); }
        }

        public CacheProfile this[object key]
        {
            get { return (CacheProfile)BaseGet(key); }
        }

        #endregion

        [ConfigurationProperty("default", DefaultValue = "default", IsRequired = false)]
        public string Default
        {
            get
            {
                return (string)base["default"];
            }
        }
    }
}