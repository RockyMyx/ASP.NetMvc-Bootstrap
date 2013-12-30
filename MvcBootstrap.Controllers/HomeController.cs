using System.Web.Mvc;
using System.Web.Caching;

namespace MvcBootstrap.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}
