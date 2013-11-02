using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;

public class BrowseAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        int roleID = 1;
        Dictionary<string, string> modules = null;
        using (DBEntity db = new DBEntity())
        {
            modules = (from m in db.Module
                       join p in db.Permission
                       on m.ID equals p.ControllerID
                       where p.RoleID == roleID && p.ActionID == 1
                       select new { m.Code, m.Name })
                       .AsEnumerable()
                       .ToDictionary(m => m.Code, m => m.Name);
        }
        foreach (KeyValuePair<string, string> module in modules)
        {
            filterContext.Controller.ViewData[module.Key] = module.Value;
        }
    }
}