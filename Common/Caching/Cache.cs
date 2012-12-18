using System;
using Common.Configuration;

namespace Common.Caching
{
    public abstract class Cache
    {
        // TODO: chenck the difference between add and set in system.runtime.cache to see if we need to provide
        // both methods or just one and which one. http://stackoverflow.com/questions/8868486/whats-the-difference-between-memorycache-add-and-memorycache-set

        public virtual void Add(string key, object value)
        {
            var profile = AppServicesConfiguration.Instance.Caching.Profiles[AppServicesConfiguration.Instance.Caching.Profiles.Default];

            Add(key, value, TimeSpan.FromSeconds(profile.SlidingExpiration));
        }

        public virtual void Add(string key, object value, string profileName)
        {
            var profile = AppServicesConfiguration.Instance.Caching.Profiles[profileName];

            Add(key, value, TimeSpan.FromSeconds(profile.SlidingExpiration));
        }

        public abstract void Add(string key, object value, DateTime absoluteExpiry);
        public abstract void Add(string key, object value, TimeSpan slidingExpiryWindow);
        
        public abstract T Get<T>(string key) where T : class;
        public abstract T Get<T>(string key, Func<T> alternateGet) where T : class;

        public abstract void InvalidateCacheItem(string key);
    }
}
