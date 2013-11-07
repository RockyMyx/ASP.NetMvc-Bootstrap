using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;
using System.Data.Objects;

public class BrowseAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        //ToTest
        //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        int roleID = 1;
        using (DBEntity db = new DBEntity())
        {
            IEnumerable<UserBrowseViewModel> modules = db.GetUserBrowse(roleID).AsEnumerable();
            foreach (UserBrowseViewModel module in modules)
            {
                filterContext.Controller.ViewData[module.Code] = module.Name;
            }
        }
    }
}