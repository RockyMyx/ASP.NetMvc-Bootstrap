using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class GenericHelper
{
    /// <summary>
    /// 遍历集合并执行Action委托
    /// </summary>
    public static void Enumerate<T>(this IEnumerable<T> collection, Action<T> doIt)
    {
        foreach (T item in collection)
        {
            doIt(item);
        }
    }

    /// <summary>
    /// 遍历集合并执行返回bool类型的Func委托
    /// </summary>
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

    /// <summary>
    /// 遍历集合，满足bool类型的Func委托的条件后，执行Action委托
    /// </summary>
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

    /// <summary>
    /// 遍历集合并执行Func委托
    /// </summary>
    public static IEnumerable<TOut> Enumerate<TIn, TOut>(this IEnumerable<TIn> collection, Func<TIn, TOut> doIt)
    {
        foreach (TIn item in collection)
            yield return doIt(item);
    }

    /// <summary>
    /// 遍历集合并根据条件过滤相应元素
    /// </summary>
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

    /// <summary>
    /// 遍历集合并过滤重复的元素
    /// </summary>
    public static IEnumerable<T> FilterRepeat<T>(this IEnumerable<T> collection)
    {
        IList<T> toCollection = new List<T>();
        collection.Enumerate(item => !toCollection.Contains(item), item => toCollection.Add(item));
        return toCollection;
    }

    /// <summary>
    /// 构建由逗号分隔的条目列表
    /// </summary>
    public static string MakeCommaString<T>(this IEnumerable<T> collection)
    {
        StringBuilder strBuiler = new StringBuilder();
        foreach (T item in collection)
        {
            strBuiler.Append(item + ",");
        }

        return strBuiler.Remove(strBuiler.Length - 1, 1).ToString();
    }

    /// <summary>
    /// 集合反序
    /// </summary>
    public static IEnumerable<T> Reverse<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            yield return list[i];
        }
    }
}
