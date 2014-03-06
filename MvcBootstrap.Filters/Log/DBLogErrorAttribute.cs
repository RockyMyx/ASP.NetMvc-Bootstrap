using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using MvcBootstrap.MysqlEFModel;

public class DBLogErrorAttribute : HandleErrorAttribute
{
    private readonly ILog _logger;

    public DBLogErrorAttribute()
    {
        _logger = LogManager.GetLogger("logerror");
    }

    public override void OnException(ExceptionContext filterContext)
    {
        log log = new log();

        //ToTest
        log.UserId = 1;
        log.UserName = "admin";

        //log.UserId = Convert.ToInt32(filterContext.HttpContext.Session["UserId"]);
        //log.UserName = filterContext.HttpContext.Session["UserName"].ToString();
        log.IpAddress = NetHelper.GetPrivateIPAddress();
        log.Controller = filterContext.GetController();
        log.Action = filterContext.GetAction();
        log.Remark = filterContext.Exception.Message;
        log.CreateDate = DateTime.Now;

        //ToTest
        //using (DBEntity db = new DBEntity())
        //{
        //    db.Log.AddObject(log);
        //    db.SaveChanges();
        //}
    }
}