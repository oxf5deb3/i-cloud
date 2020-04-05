using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace VMS.ESIApi.Utils
{
    public class DefaultMemoryCache : IMemoryCache
    {
        ConcurrentDictionary<string, DefaultCacheEntry> _cache = new ConcurrentDictionary<string, DefaultCacheEntry>();
        private static readonly object _lock = new object();
        
        public object AddOrGet(string key, object value)
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_lock, 500, ref lockTaken);
                if (lockTaken)
                {
                    if (_cache.ContainsKey(key))
                    {
                        DefaultCacheEntry entry = _cache[key] as DefaultCacheEntry;
                        if (entry != null)
                        {
                            if (entry.OffsetTime.HasValue)
                            {
                                //懒更新
                                if (entry.OffsetTime.Value < DateTime.Now)
                                {
                                    return entry.Data;
                                }
                                else
                                {
                                    return value;
                                }
                            }
                            else
                            {
                                return entry.Data;
                            }
                        }
                        else
                        {
                            return value;
                        }
                    }
                    _cache.TryAdd(key, new DefaultCacheEntry() { Data = value });

                    return value;
                }
                return value;
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_lock);
                }
            }

        }

        public object AddOrGet(string key, object value, TimeSpan? slideExpiredTime)
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_lock, 500, ref lockTaken);
                if (lockTaken)
                {
                    if (_cache.ContainsKey(key))
                    {
                        DefaultCacheEntry entry = _cache[key] as DefaultCacheEntry;
                        if (entry != null)
                        {
                            if (entry.OffsetTime.HasValue)
                            {
                                //懒更新
                                if (slideExpiredTime.HasValue && entry.OffsetTime.Value > DateTime.Now)
                                {
                                    entry.OffsetTime = DateTime.Now.Add(slideExpiredTime.Value);
                                    return entry.Data;
                                }
                                else
                                {
                                    Remove(key);
                                    return value;
                                }
                            }
                            else
                            {
                                return entry.Data;
                            }
                        }
                        else
                        {
                            return value;
                        }
                    }
                    _cache.TryAdd(key, new DefaultCacheEntry() { Data = value, OffsetTime = DateTime.Now.Add(slideExpiredTime.HasValue?slideExpiredTime.Value:TimeSpan.Zero) });

                    return value;
                }
                return value;
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_lock);
                }
            }

        }

        public void Remove(string key)
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_lock, 500, ref lockTaken);
                if (lockTaken)
                {
                    if (_cache.ContainsKey(key))
                    {
                        DefaultCacheEntry entry = new DefaultCacheEntry();
                        _cache.TryRemove(key, out entry);
                    }
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_lock);
                }
            }
        }
    }

    public class DefaultCacheEntry
    {
        public DateTimeOffset? OffsetTime { get; set; }

        public object Data { get; set; }
    }
}