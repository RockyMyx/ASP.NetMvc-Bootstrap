using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;

namespace MvcBootstrapManage.Controllers
{
    //[Log]
    //[CheckLogin]
    [ErrorCatcher]
    public class BaseController : Controller
    {
        protected DBEntity db = new DBEntity();
        protected int DataPerPage = 3;
        protected int RecordCount { get; set; }
    }
}