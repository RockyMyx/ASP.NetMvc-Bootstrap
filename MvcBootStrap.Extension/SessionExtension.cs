using System;
using System.Web;
using System.Web.SessionState;

public static class SessionExtension
{
    public static object Get(this HttpSessionState session, string name)
    {
        if (session[name] != null)
        {
            return session[name];
        }

        return null;
    }

    public static T Get<T>(this HttpSessionState session, string name, Func<T> generator)
    {
        var result = session[name];
        if (result == null)
        {
            result = generator != null ? generator() : default(T);
            session.Add(name, result);
            return (T)result;
        }

        return (T)result;
    }

    public static void Set(this HttpSessionState session, string name, object value)
    {
        if (session[name] != null)
        {
            session.Remove(name);
        }

        session.Add(name, value);
    }
}