using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using MvcBootstrap.Service;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName, string userPwd)
        {
            UserService service = new UserService();
            UserLoginViewModel user = service.GetUserLoginInfo(userName, userPwd);
            if (user != null)
            {
                Session.Add("UserId", user.UserID);
                Session.Add("RoleId", user.RoleID);
                Session.Add("RealName", user.RealName);
                Session.Add("UserName", user.UserName);
                return RedirectToAction("../Home/Index");
            }

            ViewBag.info = "用户名或密码输入错误，请重新输入！";
            return View();
        }
    }
}
