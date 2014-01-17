using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

public class CustomJsonResult : JsonResult
{
    /// <summary>
    /// 格式化字符串
    /// </summary>
    public string FormateStr
    {
        get;
        set;
    }

    public override void ExecuteResult(ControllerContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        HttpResponseBase response = context.HttpContext.Response;

        if (string.IsNullOrEmpty(this.ContentType))
        {
            response.ContentType = this.ContentType;
        }
        else
        {
            response.ContentType = "application/json";
        }

        if (this.ContentEncoding != null)
        {
            response.ContentEncoding = this.ContentEncoding;
        }

        if (this.Data != null)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string jsonString = jsSerializer.Serialize(Data);
            MatchEvaluator matchEvaluator = new MatchEvaluator(this.ConvertJsonDateToDateString);
            Regex reg = new Regex(@"\\/Date\((\d+)\)\\/");
            jsonString = reg.Replace(jsonString, matchEvaluator);
            response.Write(jsonString);
        }
    }

    /// <summary>  
    /// 将Json序列化的时间由/Date(....)转为字符串
    /// </summary>
    private string ConvertJsonDateToDateString(Match m)
    {
        string result = string.Empty;
        DateTime dt = new DateTime(1970, 1, 1);
        dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value)).ToLocalTime();
        return dt.ToString(FormateStr);
    }
}
