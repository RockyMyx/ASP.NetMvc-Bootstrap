using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;

public static class CacheExtension
{
    public const int DefaultCacheExpiration = 20;

    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, bool isReplace = false)
    {
        return cache.GetOrStore(key, generator, DefaultCacheExpiration, isReplace);
    }

    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, double expireInMinutes, bool isReplace = false)
    {
        return cache.GetOrStore(key, generator, DateTime.Now.AddMinutes(expireInMinutes), Cache.NoSlidingExpiration, isReplace);
    }

    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, TimeSpan slidingExpiration, bool isReplace = false)
    {
        return cache.GetOrStore(key, generator, Cache.NoAbsoluteExpiration, slidingExpiration, isReplace);
    }

    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, DateTime absoluteExpiration, TimeSpan slidingExpiration, bool isReplace = false)
    {
        var result = cache[key];
        if (result == null || isReplace)
        {
            result = generator != null ? generator() : default(T);
            cache.Insert(key, result, null, absoluteExpiration, slidingExpiration);
        }

        return (T)result;
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj, bool isReplace = false)
    {
        return cache.GetOrStore(key, obj, DefaultCacheExpiration, isReplace);
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj, double expireInMinutes, bool isReplace = false)
    {
        return cache.GetOrStore(key, obj, DateTime.Now.AddMinutes(expireInMinutes), Cache.NoSlidingExpiration, isReplace);
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj, TimeSpan slidingExpiration, bool isReplace = false)
    {
        return cache.GetOrStore(key, obj, Cache.NoAbsoluteExpiration, slidingExpiration, isReplace);
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj, DateTime absoluteExpiration, TimeSpan slidingExpiration, bool isReplace = false)
    {
        var result = cache[key];
        if (result == null || isReplace)
        {
            result = obj != null ? obj : default(T);
            cache.Insert(key, result, null, absoluteExpiration, slidingExpiration);
        }

        return (T)result;
    }

    public static bool IsExist(this Cache cache, string key)
    {
        return cache[key] != null;
    }

    public static void RemoveExist(this Cache cache, string key)
    {
        if (cache[key] != null) cache.Remove(key);
    }

    public static void RemoveAll(this Cache cache)
    {
        IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
        while (CacheEnum.MoveNext())
        {
            cache.Remove(CacheEnum.Key.ToString());
        }
    }
}