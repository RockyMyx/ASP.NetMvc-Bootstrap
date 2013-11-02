using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;

public class BrowseAuthorize : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        string controller = (string)filterContext.RouteData.Values["Controller"];
        int controllID = 0;
        //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        int roleID = 1;
        List<string> modules = null;
        using (DBEntity db = new DBEntity())
        {
            controllID = db.Module.Where(m => m.Controller == controller).Select(m => m.ID).FirstOrDefault();
            modules = (from m in db.Module
                       join p in db.Permission
                       on m.ID equals p.ControllerID
                       where p.RoleID == roleID && p.ActionID == 1
                       select m.Code).ToList();
        }
        foreach (string module in modules)
        {
            filterContext.Controller.ViewData["browse-" + module] = module;
        }
    }
}