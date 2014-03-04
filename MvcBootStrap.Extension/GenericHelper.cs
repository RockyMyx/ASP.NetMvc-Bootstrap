using System;
using System.Collections.Generic;
using System.Text;

public static class GenericHelper
{
    public static void Enumerate<T>(this IEnumerable<T> collection, Action<T> doIt)
    {
        foreach (T item in collection)
        {
            doIt(item);
        }
    }

    public static bool Enumerate<T>(this IEnumerable<T> collection, Func<T, bool> filter)
    {
        foreach (T item in collection)
        {
            if (filter(item))
            {
                return true;
            }
        }

        return false;
    }

    public static void Enumerate<T>(this IEnumerable<T> collection, Func<T, bool> filter, Action<T> doIt)
    {
        foreach (T item in collection)
        {
            if (filter(item))
            {
                doIt(item);
            }
        }
    }

    public static IEnumerable<TOut> Enumerate<TIn, TOut>(this IEnumerable<TIn> collection, Func<TIn, TOut> doIt)
    {
        foreach (TIn item in collection)
            yield return doIt(item);
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, bool> filter)
    {
        foreach (T item in collection)
        {
            if (filter(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> FilterRepeat<T>(this IEnumerable<T> collection)
    {
        IList<T> toCollection = new List<T>();
        collection.Enumerate(item => !toCollection.Contains(item), item => toCollection.Add(item));
        return toCollection;
    }

    public static string MakeCommaString<T>(this IEnumerable<T> collection)
    {
        StringBuilder strBuiler = new StringBuilder();
        foreach (T item in collection)
        {
            strBuiler.Append(item + ",");
        }

        return strBuiler.Remove(strBuiler.Length - 1, 1).ToString();
    }

    public static IEnumerable<T> Reverse<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            yield return list[i];
        }
    }
}
