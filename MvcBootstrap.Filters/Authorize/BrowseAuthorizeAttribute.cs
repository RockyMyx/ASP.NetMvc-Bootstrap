using System.Collections.Generic;
using System.Web.Mvc;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
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