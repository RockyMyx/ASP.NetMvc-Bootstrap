using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class ConvertHelper
{
    public static bool ObjToBool(this object obj)
    {
        bool flag;
        if (obj == null)
        {
            return false;
        }
        if (obj.Equals(DBNull.Value))
        {
            return false;
        }
        return (bool.TryParse(obj.ToString(), out flag) && flag);
    }

    public static DateTime? ObjToDateNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }
        try
        {
            return new DateTime?(Convert.ToDateTime(obj));
        }
        catch (ArgumentNullException ex)
        {
            return null;
        }
    }

    public static int ObjToInt(this object obj)
    {
        if (obj != null)
        {
            int num;
            if (obj.Equals(DBNull.Value))
            {
                return 0;
            }
            if (int.TryParse(obj.ToString(), out num))
            {
                return num;
            }
        }
        return 0;
    }

    public static long ObjToLong(this object obj)
    {
        if (obj != null)
        {
            long num;
            if (obj.Equals(DBNull.Value))
            {
                return 0;
            }
            if (long.TryParse(obj.ToString(), out num))
            {
                return num;
            }
        }
        return 0;
    }

    public static int? ObjToIntNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }
        if (obj.Equals(DBNull.Value))
        {
            return null;
        }
        return new int?(ObjToInt(obj));
    }

    public static string ObjToStr(this object obj)
    {
        if (obj == null)
        {
            return "";
        }
        if (obj.Equals(DBNull.Value))
        {
            return "";
        }
        return Convert.ToString(obj);
    }

    public static decimal ObjToDecimal(this object obj)
    {
        if (obj == null)
        {
            return 0M;
        }
        if (obj.Equals(DBNull.Value))
        {
            return 0M;
        }
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch
        {
            return 0M;
        }
    }

    public static decimal? ObjToDecimalNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }
        if (obj.Equals(DBNull.Value))
        {
            return null;
        }
        return new decimal?(ObjToDecimal(obj));
    }
}