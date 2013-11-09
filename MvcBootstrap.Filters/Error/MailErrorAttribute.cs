using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

public class MailErrorAttribute : FilterAttribute, IExceptionFilter
{
    #region IExceptionFilter Members

    public void OnException(ExceptionContext filterContext)
    {
        StringBuilder strBuilder = new StringBuilder("异常信息：\r\n");

        string controllerName = filterContext.GetController();
        string actionName = filterContext.GetAction();

        strBuilder.AppendFormat("\tController：{0}\r\n", controllerName);
        strBuilder.AppendFormat("\tAction：{0}\r\n", actionName);
        strBuilder.AppendFormat("\tExceptionInfo：{0}\r\n", filterContext.Exception);

        //ToTest
        //MailHelper mail = new MailHelper("smtp.gmail.com", "myx8178633@gmail.com", "gzlpsmyx");
        //mail.Send("System", "myx8178633@163.com", "资产系统出现错误！", strBuilder.ToString());
    }

    #endregion
}
