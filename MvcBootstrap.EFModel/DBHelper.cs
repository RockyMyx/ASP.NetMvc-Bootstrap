using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

public static class DBHelper
{
    public static IEnumerable<T> GetEntities<T>(this IEnumerable<T> model, Func<T, bool> where)
    {
        return model.Where(where);
    }

    public static T GetEntity<T>(this IEnumerable<T> model, Func<T, bool> where)
    {
        return model.Where(where).SingleOrDefault();
    }

    public static int GetEntityField<T>(this IEnumerable<T> model, Func<T, bool> where, Func<T, int> select)
    {
        return model.Where(where).Select(select).SingleOrDefault();
    }

    public static string GetEntityField<T>(this IEnumerable<T> model, Func<T, bool> where, Func<T, string> select)
    {
        return model.Where(where).Select(select).SingleOrDefault();
    }

    public static IEnumerable<T> GetPagingInfo<T>(this IEnumerable<T> model, Func<T, int> orderby, int pageIndex, int pageSize)
    {
        return model.OrderBy(orderby).Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    public static IEnumerable<T> GetPagingInfo<T>(this IEnumerable<T> model, int pageIndex, int pageSize)
    {
        return model.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    public static IEnumerable<T> GetPagingInfo<T>(this IEnumerable<T> model, int pageSize)
    {
        return model.Take(pageSize);
    }
}
