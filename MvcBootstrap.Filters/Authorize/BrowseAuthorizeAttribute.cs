using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.MysqlEFModel;
using System.Data.Objects;
using MvcBootstrap.Service;

public class BrowseAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        //ToTest
        //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        int roleID = 1;
        UserService service = new UserService();
        IEnumerable<UserBrowseViewModel> modules = service.GetUserBrowse(roleID);
        foreach (UserBrowseViewModel module in modules)
        {
            filterContext.SetViewData(module.Code, module.Name);
        }
    }
}