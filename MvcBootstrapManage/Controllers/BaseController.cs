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
    [OperationAuthorize]
    [BrowseAuthorize]
    public class BaseController : Controller
    {
    }
}