using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.Controllers
{
    //[CheckLogin]
    [BrowseAuthorize]
    public class BaseController : Controller
    {
    }
}