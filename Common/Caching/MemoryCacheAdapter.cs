using System;
using System.Runtime.Caching;

namespace Common.Caching
{
    public class MemoryCacheAdapter : Cache
    {
        private readonly MemoryCache memoryCache = MemoryCache.Default;

        public void Add(string key, object value, DateTime absoluteExpiry)
        {
            memoryCache.Add(key, value, new CacheItemPolicy { AbsoluteExpiration = new DateTimeOffset(absoluteExpiry) });
        }

        public void Add(string key, object value, TimeSpan slidingExpiryWindow)
        {
            memoryCache.Add(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiryWindow });
        }

        public void Add(string key, object value)
        {
            memoryCache.Add(key, value, new CacheItemPolicy());
        }

        public T Get<T>(string key) where T : class
        {
            return memoryCache.Get(key) as T;
        }

        public T Get<T>(string key, Func<T> alternateGet) where T : class
        {
            var value = memoryCache.Get(key) as T;

            if (value == null)
            {
                value = alternateGet.Invoke();
                memoryCache.Add(key, value, new CacheItemPolicy());
            }

            return value;
        }

        public void InvalidateCacheItem(string key)
        {
            if (!memoryCache.Contains(key)) return;

            memoryCache.Remove(key);
        }
    }
}
