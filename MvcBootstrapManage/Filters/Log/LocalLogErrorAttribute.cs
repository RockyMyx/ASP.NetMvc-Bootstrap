using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

public class LocalLogErrorAttribute : HandleErrorAttribute
{
    private readonly ILog _logger;

    public LocalLogErrorAttribute()
    {
        _logger = LogManager.GetLogger("logerror");
    }

    public override void OnException(ExceptionContext filterContext)
    {
        if (filterContext.ExceptionHandled) return;
        if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500) return;
        if (!ExceptionType.IsInstanceOfType(filterContext.Exception)) return;

        string controllerName = filterContext.RouteData.Values["controller"].ToString();
        string actionName = filterContext.RouteData.Values["action"].ToString();
        HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

        filterContext.Result = new ViewResult
        {
            ViewName = View,
            MasterName = Master,
            ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
            TempData = filterContext.Controller.TempData
        };

        filterContext.Controller.ViewData["Ex"] = filterContext.Exception;

        _logger.Error(filterContext.Exception.Message, filterContext.Exception);

        filterContext.HttpContext.Response.Clear();
        filterContext.HttpContext.Response.StatusCode = 500;
        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        filterContext.ExceptionHandled = true;
    }
}