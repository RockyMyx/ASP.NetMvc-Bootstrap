using System.Web.Mvc;

public static class ControllerContextHelper
{
    public static string GetController(this ControllerContext controllContext)
    {
        return controllContext.RouteData.Values["Controller"].ToString();
    }

    public static string GetAction(this ControllerContext controllContext)
    {
        return controllContext.RouteData.Values["Action"].ToString();
    }

    public static void SetViewData(this ControllerContext controllContext, string key, object value)
    {
        controllContext.Controller.ViewData[key] = value;
    }
}