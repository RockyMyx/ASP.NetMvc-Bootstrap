using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using MvcBootstrap.Service;

public class OperationAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        //ToTest
        //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
        int roleID = 1;

        string controller = filterContext.GetController();
        ModuleService service = new ModuleService();
        int controllerID = service.GetControllerIDByName(controller);
        IEnumerable<string> actions = db.GetUserOperation(roleID, controllerID);
        foreach (string action in actions)
        {
            filterContext.SetViewData(action, true);
        }
    }
}
