using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;

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
            using (DBEntity db = new DBEntity())
            {
                //DB操作方法一：使用原生SQL
                //string sql = "select count(*) from User where Name='admin' and Password='admin'";
                //db.ExecuteStoreQuery<int>(sql).FirstOrDefault();
                //db.ExecuteStoreCommand(sql);

                //DB操作方法二：使用参数化SQL
                //string sql = "select count(*) from User where Name=@Name and Password=@Password";
                //var args = new DbParameter[] { new SqlParameter { ParameterName = "Name", Value = "admin" }, new SqlParameter { ParameterName = "Password", Value = "admin" } };
                //db.ExecuteStoreQuery<User>(sql, args);

                //DB操作方法三：使用LINQ
                var user = (from u in db.User
                            join r in db.UserRole
                            on u.ID equals r.UserID
                            where (u.Name == userName && u.Password == userPwd)
                            select new 
                            {
                                UserID = u.ID,
                                RoleID = r.RoleID,
                                RealName = u.RealName,
                                UserName = u.Name
                            }).FirstOrDefault();
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
}
