using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Helpers;

public class ErrorCatcherAttribute : FilterAttribute, IExceptionFilter
{
    #region IExceptionFilter Members

    public void OnException(ExceptionContext filterContext)
    {
        filterContext.ExceptionHandled = true;
        filterContext.Result = new ViewResult()
        {
            ViewName = "Error",
            ViewData = filterContext.Controller.ViewData
        };
        filterContext.Controller.ViewData["Ex"] = filterContext.Exception;


        //MailHelper mail = new MailHelper("smtp.gmail.com", "myx8178633@gmail.com", "gzlpsmyx", new List<System.Net.Mail.Attachment>() { new System.Net.Mail.Attachment(xxx)) });
        //mail.Send("SenderName", "myx8178633@163.com", "Title", "Content");
    }
    #endregion
}
