using System;

namespace Common.Caching
{
    public interface Cache
    {        
        void Add(string key, object value, DateTime absoluteExpiry);
        void Add(string key, object value, TimeSpan slidingExpiryWindow);
        void Add(string key, object value);

        T Get<T>(string key) where T : class;

        void InvalidateCacheItem(string key);
    }
}
