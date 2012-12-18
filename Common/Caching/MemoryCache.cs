using System;
using System.Configuration;
using System.Runtime.Caching;
using Common.Configuration;

namespace Common.Caching
{
    public class MemoryCache : Cache
    {
        private readonly System.Runtime.Caching.MemoryCache memoryCache = System.Runtime.Caching.MemoryCache.Default;

        public override void Add(string key, object value, DateTime absoluteExpiry)
        {
            memoryCache.Add(key, value, new CacheItemPolicy { AbsoluteExpiration = new DateTimeOffset(absoluteExpiry) });
        }

        public override void Add(string key, object value, TimeSpan slidingExpiryWindow)
        {
            memoryCache.Add(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiryWindow });
        }

        public override T Get<T>(string key) 
        {
            return memoryCache.Get(key) as T;
        }

        public override T Get<T>(string key, Func<T> alternateGet) 
        {
            var value = memoryCache.Get(key) as T;

            if (value == null)
            {
                value = alternateGet.Invoke();
                memoryCache.Add(key, value, new CacheItemPolicy());
            }

            return value;
        }

        public override void InvalidateCacheItem(string key)
        {
            if (!memoryCache.Contains(key)) return;

            memoryCache.Remove(key);
        }
    }
}
