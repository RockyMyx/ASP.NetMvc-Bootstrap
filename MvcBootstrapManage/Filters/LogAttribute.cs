using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class LogAttribute : FilterAttribute, IActionFilter
{
    #region IActionFilter Members

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //TODO
    }

    #endregion
}
