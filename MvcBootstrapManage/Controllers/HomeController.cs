using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;

namespace MvcBootstrapManage.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            //test
            ViewData["Source"] = "[1,2,3,4,5,6]";
            return View();
        }
    }
}
