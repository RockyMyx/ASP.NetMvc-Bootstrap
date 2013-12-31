using System;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web;

public class ResultCacheAttribute : ActionFilterAttribute
{
    public string CacheKey { get; private set; }
    public int ExpireMinutes { get; set; }
    public CacheDependency Dependency  { get; set; }

    private CacheItemPriority _priority = CacheItemPriority.Default;
    public CacheItemPriority Priority
    {
        get
        {
            return _priority;
        }
        set
        {
            _priority = value;
        }
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        HttpContextBase context = filterContext.HttpContext;
        string url = context.Request.Url.PathAndQuery;
        this.CacheKey = "ResultCache-" + url;
        if (filterContext.HttpContext.Cache[this.CacheKey] != null)
        {
            //Setting the result prevents the action itself to be executed
            filterContext.Result = (ActionResult)context.Cache[this.CacheKey];
        }

        base.OnActionExecuting(filterContext);
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        filterContext.Controller.ViewData["CachedStamp"] = DateTime.Now;
        filterContext.HttpContext.Cache.Add(this.CacheKey, filterContext.Result, Dependency, DateTime.Now.AddMinutes(ExpireMinutes), System.Web.Caching.Cache.NoSlidingExpiration, Priority, null);

        base.OnActionExecuted(filterContext);
    }
}
