using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class CheckLoginAttribute : FilterAttribute, IActionFilter
{
    #region IActionFilter Members

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Today.AddYears(-2));
        if (filterContext.HttpContext.Session["UserId"] == null)
        {
            filterContext.HttpContext.Session.Clear();
            filterContext.HttpContext.Request.Cookies.Clear();
            filterContext.HttpContext.Response.Redirect("/Login/Index");
        }
    }

    #endregion
}
