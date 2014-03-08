using System.Collections.Generic;
using System.Web.Mvc;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.Service;

public class OperationAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        //ToTest
        //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        int roleID = 1;
        
        string controller = filterContext.GetController();
        ModuleService moduleService = new ModuleService();
        int controllerID = moduleService.GetModuleIdByName(controller);
        UserService userService = new UserService();
        IEnumerable<string> actions = userService.GetUserOperation(roleID, controllerID);
        foreach (string action in actions)
        {
            filterContext.SetViewData(action, true);
        }
    }
}
