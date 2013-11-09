using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Collections;

public class CacheHelper
{
    public static object Get(string key)
    {
        Cache objCache = HttpRuntime.Cache;
        return objCache[key];
    }

    public static void Set(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        if (HttpRuntime.Cache[key] == null && value != null)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(key, value, null, absoluteExpiration, slidingExpiration);
        }
    }

    public static void Set(string key, object value, TimeSpan slidingExpiration)
    {
        if (HttpRuntime.Cache[key] == null && value != null)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(key, value, null, DateTime.MaxValue, slidingExpiration, CacheItemPriority.NotRemovable, null);
        }
    }

    public static void Set(string key, object value)
    {
        if (HttpRuntime.Cache[key] == null && value != null)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(key, value);
        }
    }

    public static object Remove(string key)
    {
        if (HttpRuntime.Cache[key] != null)
        {
            return HttpRuntime.Cache.Remove(key);
        }
        else
        {
            return null;
        }
    }

    public static void RemoveAll()
    {
        IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
        while (CacheEnum.MoveNext())
        {
            HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
        }
    }
}
