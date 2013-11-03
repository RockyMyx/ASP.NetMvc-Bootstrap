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
        using (DBEntity db = new DBEntity())
        {
            //int roleID = Convert.ToInt32(HttpContext.Current.Session["RoleID"]);
            int roleID = 1;

            string controller = (string)filterContext.RouteData.Values["Controller"];
            int controllerID = db.Module.Where(m => m.Controller == controller).Select(m => m.ID).Single();

            List<string> actions = db.GetUserOperation(roleID, controllerID).ToList();
            foreach (string action in actions)
            {
                filterContext.Controller.ViewData[action] = true;
            }
        }
    }
}
