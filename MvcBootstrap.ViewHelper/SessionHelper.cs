using System.Web;

public class SessionHelper
{
    public static object Get(string name)
    {
        if (HttpContext.Current.Session[name] != null)
        {
            return HttpContext.Current.Session[name];
        }

        return null;
    }

    public static void Set(string name, object value)
    {
        if (HttpContext.Current.Session[name] != null)
        {
            HttpContext.Current.Session.Remove(name);
        }

        HttpContext.Current.Session.Add(name, value);
    }

    public static void Remove(string name)
    {
        if (HttpContext.Current.Session != null && HttpContext.Current.Session[name] != null)
        {
            HttpContext.Current.Session[name] = null;
        }
    }
}
