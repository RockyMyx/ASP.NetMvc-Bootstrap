using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using MvcBootstrapManage.Models;

public class DBLogErrorAttribute : HandleErrorAttribute
{
    private readonly ILog _logger;

    public DBLogErrorAttribute()
    {
        _logger = LogManager.GetLogger("logerror");
    }

    public override void OnException(ExceptionContext filterContext)
    {
        Log log = new Log();

        //ToTest
        log.UserId = 1;
        log.UserName = "admin";

        //log.UserId = Convert.ToInt32(filterContext.HttpContext.Session["UserId"]);
        //log.UserName = filterContext.HttpContext.Session["UserName"].ToString();
        log.IpAddress = NetHelper.GetPrivateIPAddress();
        log.Controller = filterContext.RouteData.Values["controller"].ToString();
        log.Action = filterContext.RouteData.Values["action"].ToString();
        log.Remark = filterContext.Exception.Message;
        log.CreateDate = DateTime.Now;

        //using (DBEntity db = new DBEntity())
        //{
        //    db.Log.AddObject(log);
        //    db.SaveChanges();
        //}
    }
}