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
        private int _size = 0;
        private const int MAX_SIZE = 1 << 30;
        private List<string> _keys = new List<string>();
        private DateTime _lastClearExpireTime = DateTime.Now;
        private int _clearExpireCheckTime = 120;

        public int Count()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_lock, 500, ref lockTaken);
                if (lockTaken)
                {
                    return _size;
                }
                return -1;
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_lock);
                }
            }

        }

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
                                    if ((DateTime.Now - _lastClearExpireTime).Minutes > _clearExpireCheckTime)
                                    {
                                        ClearExpiredData();
                                        _lastClearExpireTime = DateTime.Now;
                                    }
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
                    _size++;
                    _keys.Add(key);
                    if ((DateTime.Now - _lastClearExpireTime).Minutes > _clearExpireCheckTime)
                    {
                        ClearExpiredData();
                        _lastClearExpireTime = DateTime.Now;
                    }
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
                                    if ((DateTime.Now - _lastClearExpireTime).Minutes > _clearExpireCheckTime)
                                    {
                                        ClearExpiredData();
                                        _lastClearExpireTime = DateTime.Now;
                                    }
                                    return entry.Data;
                                }
                                else
                                {
                                    //Remove(key);
                                    DefaultCacheEntry entry1 = new DefaultCacheEntry();
                                    if(_cache.TryRemove(key, out entry1))
                                    {
                                        _size--;
                                        _keys.Remove(key);
                                    }
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
                    _cache.TryAdd(key, new DefaultCacheEntry() { Data = value, OffsetTime = DateTime.Now.Add(slideExpiredTime.HasValue ? slideExpiredTime.Value : TimeSpan.Zero) });
                    _size++;
                    _keys.Add(key);
                    if ((DateTime.Now - _lastClearExpireTime).Minutes > _clearExpireCheckTime)
                    {
                        ClearExpiredData();
                        _lastClearExpireTime = DateTime.Now;
                    }
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
                        _keys.Remove(key);
                        _size--;
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

        private void ClearExpiredData()
        {
            var now = DateTime.Now;
            var removes = new List<string>();
            foreach(var k in _keys)
            {
                DefaultCacheEntry entry = new DefaultCacheEntry();
                var success = _cache.TryGetValue(k, out entry);
                if (success)
                {
                    if (entry.OffsetTime > now)
                    {
                        removes.Add(k);
                    }
                }
            }
            if (removes.Count > 0)
            {
                foreach (var r in removes)
                {
                    DefaultCacheEntry entry = new DefaultCacheEntry();
                    var success = _cache.TryRemove(r, out entry);
                    if (success)
                    {
                        _size--;
                        _keys.Remove(r);
                    }
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