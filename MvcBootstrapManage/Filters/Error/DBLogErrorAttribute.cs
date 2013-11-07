using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

public class DBLogErrorAttribute : HandleErrorAttribute
{
    private readonly ILog _logger;

    public DBLogErrorAttribute()
    {
        _logger = LogManager.GetLogger("logerror");
    }

    public override void OnException(ExceptionContext filterContext)
    {
        if (filterContext.ExceptionHandled)
        {
            return;
        }

        if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
        {
            return;
        }

        if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
        {
            return;
        }

        //TODO
        //插入数据库Log表
    }
}