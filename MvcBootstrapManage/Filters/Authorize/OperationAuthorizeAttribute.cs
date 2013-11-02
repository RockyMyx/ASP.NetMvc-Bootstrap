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

        List<string> Actions = null;
        using (DBEntity db = new DBEntity())
        {
            int controllerID = db.Module.Where(m => m.Controller == controller).Select(m => m.ID).Single();
            //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
            int roleID = 1;
            Actions = (from o in db.Operation
                       join p in db.Permission
                       on o.ID equals p.ActionID
                       where p.RoleID == roleID && p.ControllerID == controllerID && o.ID != 1
                       select o.Action).ToList();
        }

        foreach (string action in Actions)
        {
            filterContext.Controller.ViewData[action] = true;
        }
    }
}
