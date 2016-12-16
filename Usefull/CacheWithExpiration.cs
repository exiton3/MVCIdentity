namespace Edi.Advance.Framework.Infrastructure.Utils
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public static class SimpleCache
    {
        static ICache<string, object> cache;
        static SimpleCache()
        {
            cache = Cache.Timed<string, object>(TimeSpan.FromMinutes(5));
        }

        public static T Get<T>(string key, Func<T> builder) where T : class
        {
            return (T)cache.Get(key, builder);
        }
    }

    public static class Cache
    {
        public static ICache<TKey, TValue> Timed<TKey, TValue>(TimeSpan expirationInterval)
        {
            return new TimedCache<TKey, TValue>(expirationInterval);
        }
    }

    public interface ICache<in TKey, TValue>
    {
        void AddOrUpdate(TKey key, Func<TValue> builder);
        TValue Get(TKey key);
        TValue Get(TKey key, Func<TValue> builder);
    }

    public class TimedCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private readonly TimeSpan expirationInterval;
        private readonly TimeSpan timerInterval;
        private readonly ConcurrentDictionary<TKey, CacheItem<TValue>> values = new ConcurrentDictionary<TKey, CacheItem<TValue>>();
        private readonly Timer timer;

        public TimedCache(TimeSpan expirationInterval)
        {
            this.expirationInterval = expirationInterval;
            timerInterval = TimeSpan.FromMilliseconds(expirationInterval.TotalMilliseconds / 2);
            timer = new Timer(Cleanup, null, timerInterval, timerInterval);
        }

        public void AddOrUpdate(TKey key, Func<TValue> builder)
        {
            values.AddOrUpdate(key, x => new CacheItem<TValue>(builder), (x, v) => new CacheItem<TValue>(builder));
        }

        public TValue Get(TKey key)
        { 
            CacheItem<TValue> item;
            values.TryGetValue(key, out item);

            return item == null ? default(TValue) : item.Value;
        }

        public TValue Get(TKey key, Func<TValue> builder)
        {
            return values
                .GetOrAdd(key, k => new CacheItem<TValue>(builder))
                .Value;
        }

        private void Cleanup(object state)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            var now = DateTime.UtcNow;
            var toRemove = values
                .Where(cacheItem => cacheItem.Value.Timestamp + expirationInterval < now)
                .Select(cacheItem => cacheItem.Key)
                .ToList();

            CacheItem<TValue> dummy;
            foreach (var key in toRemove)
                values.TryRemove(key, out dummy);

            timer.Change(timerInterval, timerInterval);
        }
    }

    public class CacheItem<TValue>
    {
        private readonly Lazy<TValue> value;

        public CacheItem(Func<TValue> valueBuilder)
        {
            value = new Lazy<TValue>(valueBuilder, LazyThreadSafetyMode.ExecutionAndPublication);
            Timestamp = DateTime.UtcNow;
        }

        public TValue Value { get { return value.Value; } }
        public DateTime Timestamp { get; private set; }
    }
}