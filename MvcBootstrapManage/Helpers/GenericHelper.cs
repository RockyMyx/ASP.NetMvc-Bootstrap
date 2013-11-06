using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using MvcBootstrapManage.Models;

public static class DBHelper
{
    public static IEnumerable<T> GetEntities<T>(this IEnumerable<T> model, Func<T, bool> exp)
    {
        using (DBEntity db = new DBEntity())
        {
            return model.Where(exp);
        }
    }

    public static T GetEntity<T>(this IEnumerable<T> model, Func<T, bool> exp)
    {
        using (DBEntity db = new DBEntity())
        {
            return model.Where(exp).SingleOrDefault();
        }
    }

    public static IEnumerable<T> GetPagingInfo<T>(this IEnumerable<T> model, Func<T, int> exp, int pageIndex, int pageSize)
    {
        return model.OrderBy(exp).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
    }

    public static IEnumerable<T> GetPagingInfo<T>(this IEnumerable<T> model, int pageIndex, int pageSize)
    {
        return model.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
    }

    public static IEnumerable<T> GetPagingInfo<T>(this IEnumerable<T> model, int pageSize)
    {
        return GetPagingInfo<T>(model, 1, pageSize);
    }
}
