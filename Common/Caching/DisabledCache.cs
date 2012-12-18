using System;

namespace Common.Caching
{
    /// <summary>
    /// Null object implementation of cache for disabled cache.
    /// </summary>
    public class DisabledCache : Cache
    {
        public override void Add(string key, object value, DateTime absoluteExpiry) { }

        public override void Add(string key, object value, TimeSpan slidingExpiryWindow) { }

        public override void Add(string key, object value) { }

        public override void Add(string key, object value, string profileName) { }

        public override T Get<T>(string key) { return null; }

        public override T Get<T>(string key, Func<T> alternateGet)
        {
            return alternateGet.Invoke();
        }

        public override void InvalidateCacheItem(string key) { }
    }
}
