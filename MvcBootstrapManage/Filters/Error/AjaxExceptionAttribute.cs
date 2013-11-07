using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class AjaxExceptionAttribute : FilterAttribute, IExceptionFilter
{
    public void OnException(ExceptionContext filterContext)
    {
        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
        {
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new JsonResult
            {
                Data = new
                {
                    errorMessage = "AJAX ERROR —— " + filterContext.Exception.Message
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}