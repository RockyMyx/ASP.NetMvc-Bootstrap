using System.Web.Mvc;
using System.Text;

namespace MvcBootstrap.Controllers
{
    //[CheckLogin]
    public class BaseController : Controller
    {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }

        protected JsonResult MyJson(object data, JsonRequestBehavior behavior, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormateStr = format
            };
        }

        protected JsonResult MyJson(object data, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                FormateStr = format
            };
        }

        protected JsonResult MyJson(object data)
        {
            return new CustomJsonResult
            {
                Data = data,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }
    }
}