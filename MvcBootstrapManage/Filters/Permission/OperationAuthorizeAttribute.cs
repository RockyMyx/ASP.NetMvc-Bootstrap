using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;

public class OperationAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        string controller = (string)filterContext.RouteData.Values["Controller"];
        int controllID = 0;
        //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        int roleID = 1;
        List<string> Actions = null;
        using (DBEntity db = new DBEntity())
        {
            controllID = db.Module.Where(m => m.Controller == controller).Select(m => m.ID).FirstOrDefault();
            Actions = (from o in db.Operation
                       join p in db.Permission
                       on o.ID equals p.ActionID
                       where p.RoleID == roleID && o.ID != 1
                       select o.Action).ToList();
        }
        foreach (string action in Actions)
        {
            filterContext.Controller.ViewData[action] = true;
        }

        /*string fullName = filterContext.HttpContext.User.Identity.Name;

        if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            filterContext.HttpContext.Response.Redirect(string.Format("~/Account/LogOn?from={0}", filterContext.HttpContext.Request.Url.PathAndQuery));
        else
        {
            var r = new BLL.RoleGroupRepository().GetUserRoleGroup(fullName);
            var q = new BLL.ViewRoleGroupRepository().GetCacheAll().FindAll(c =>
                c.RoleID == r.RoleID && c.SysAppController == controller && c.SysAppAction == action &&
                (c.StartTime.Equals("all", StringComparison.CurrentCultureIgnoreCase) ? true : (DateTime.Parse(c.StartTime) <= DateTime.Now)) &&
                (c.EndTime.Equals("all", StringComparison.CurrentCultureIgnoreCase) ? true : (DateTime.Parse(c.EndTime) >= DateTime.Now)));
            if (q.Count == 0)
                throw new Exception(string.Format("【用户:{0}】对不起，您没有访问该页面的权限。", fullName));
            else
            {
                string from = filterContext.HttpContext.Request.Params["from"];
                filterContext.HttpContext.Response.Redirect((string.IsNullOrEmpty(from) ? "~/Home/Index" : from));
            }
        }*/
    }
}
