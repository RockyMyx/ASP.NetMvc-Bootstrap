using System.Web;
using System.Web.Mvc;

namespace MvcBootstrapManage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalLogErrorAttribute());
            filters.Add(new DBLogErrorAttribute());
            filters.Add(new MailErrorAttribute());
            filters.Add(new AjaxExceptionAttribute());
        }
    }
}