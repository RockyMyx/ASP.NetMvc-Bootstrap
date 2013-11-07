using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

public static class RouteDataHelper
{
    public static string GetController(this RouteData routeData)
    {
        return routeData.Values["Controller"].ToString();
    }

    public static string GetAction(this RouteData routeData)
    {
        return routeData.Values["Action"].ToString();
    }
}