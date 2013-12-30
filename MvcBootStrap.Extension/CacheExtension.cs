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

    // var user = HttpRuntime.Cache
    //            .GetOrStore<User>
    //            (string.Format("User{0}", _userId), 
    //            () => Repository.GetUser(_userId));
    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator)
    {
        return cache.GetOrStore(key, generator, DefaultCacheExpiration);
    }

    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, double expireInMinutes)
    {
        return cache.GetOrStore(key, generator, DateTime.Now.AddMinutes(expireInMinutes), Cache.NoSlidingExpiration);
    }

    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, TimeSpan slidingExpiration)
    {
        return cache.GetOrStore(key, generator, Cache.NoAbsoluteExpiration, slidingExpiration);
    }

    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        var result = cache[key];
        if (result == null)
        {
            result = generator != null ? generator() : default(T);
            cache.Insert(key, result, null, absoluteExpiration, slidingExpiration);
        }

        return (T)result;
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj)
    {
        return cache.GetOrStore(key, obj, DefaultCacheExpiration);
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj, double expireInMinutes)
    {
        return cache.GetOrStore(key, obj, DateTime.Now.AddMinutes(expireInMinutes), Cache.NoSlidingExpiration);
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj, TimeSpan slidingExpiration)
    {
        return cache.GetOrStore(key, obj, Cache.NoAbsoluteExpiration, slidingExpiration);
    }

    public static T GetOrStore<T>(this Cache cache, string key, T obj, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        var result = cache[key];
        if (result == null)
        {
            result = obj != null ? obj : default(T);
            cache.Insert(key, result, null, absoluteExpiration, slidingExpiration);
        }

        return (T)result;
    }

    public static void Remove(this Cache cache, string key)
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