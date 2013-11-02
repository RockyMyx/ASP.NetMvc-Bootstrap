using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace MvcBootstrapManage.Helpers
{
    public class CacheHelper
    {
        public static object TryAddCache(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
            TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemovedCallback)
        {
            if (HttpRuntime.Cache[key] == null && value != null)
                return HttpRuntime.Cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemovedCallback);
            else
                return null;
        }

        public static object TryRemoveCache(string key)
        {
            if (HttpRuntime.Cache[key] != null)
                return HttpRuntime.Cache.Remove(key);
            else
                return null;
        }

        /// <summary>
        /// 移除键中带某关键字的缓存
        /// </summary>
        public static void RemoveCache(string keyInclude)
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                if (CacheEnum.Key.ToString().IndexOf(keyInclude.ToString()) >= 0)
                    HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }

        public static void RemoveAllCache()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}