using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Collections;
using MvcBootstrapManage.Models;

public static class ViewHelper
{
    /// <summary>
    /// 生成权限操作选项
    /// </summary>
    public static MvcHtmlString Operations(this HtmlHelper helper)
    {
        using (DBEntity db = new DBEntity())
        {
            IDictionary<int, string> operations = db.Operation
                                                    .Select(s => new { s.ID, s.Name })
                                                    .AsEnumerable()
                                                    .ToDictionary(k => k.ID, k => k.Name);
            string label = "<label class=\"checkbox inline\"><input type=\"checkbox\" id=\"op{0}\" name=\"op{0}\" />{1}</label>";
            StringBuilder strBuilder = new StringBuilder();
            foreach (KeyValuePair<int, string> item in operations)
            {
                strBuilder.AppendFormat(label, item.Key, item.Value);
            }

            return MvcHtmlString.Create(strBuilder.ToString());
        }
    }
}