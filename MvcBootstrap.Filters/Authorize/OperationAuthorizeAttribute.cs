using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;

public class OperationAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        using (DBEntity db = new DBEntity())
        {
            //ToTest
            //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
            int roleID = 1;

            string controller = filterContext.GetController();
            int controllerID = db.Module.GetEntityField(m => m.Controller == controller, m => m.ID);
            IEnumerable<string> actions = db.GetUserOperation(roleID, controllerID);
            foreach (string action in actions)
            {
                filterContext.SetViewData(action, true);
            }
        }
    }
}
