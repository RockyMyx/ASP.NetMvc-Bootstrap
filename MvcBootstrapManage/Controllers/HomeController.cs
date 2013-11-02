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

        protected override int RecordCount
        {
            get
            {
                return db.Module.Count();
            }
        }

        public ActionResult Welcome()
        {
            ViewData["Source"] = "[1,2,3,4,5]";
            return View();
        }

        public ActionResult Test(int? id)
        {
            IEnumerable<Student> students = new List<Student>()
                {
                    new Student{ Id=0, Name="John"},
                    new Student{ Id=1, Name="Marry"},
                    new Student{ Id=2, Name="Andy"},
                    new Student{ Id=3, Name="Tom"},
                    new Student{ Id=4, Name="Lydia"},
                    new Student{ Id=5, Name="Chris"},
                    new Student{ Id=6, Name="Justin"},
                    new Student{ Id=7, Name="Susan"}
                };
            PagingHelper<Student> model = new PagingHelper<Student>(2, students);
            model.PageIndex = id ?? 0;
            return View(model);
        }

        public ActionResult TestLazy()
        {
            var model = new MyViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult TestLazy(int year)
        {
            var model = new[]
            {
                new TheData { Year = year, Foo = "foo 1", Bar = "bar 1" },
                new TheData { Year = year, Foo = "foo 2", Bar = "bar 2" },
                new TheData { Year = year, Foo = "foo 3", Bar = "bar 3" },
            };
            return PartialView("_data", model);
        }

        public ActionResult Manage()
        {
            using (DBEntity db = new DBEntity())
            {
                var result = db.Module.Select(m => m).ToList();
                return View(result);
            }
        }

        public ActionResult ManageLazy()
        {
            using (DBEntity db = new DBEntity())
            {
                ViewBag.totalCount = this.RecordCount;
                var result = db.Module.OrderBy(m => m.ID).Skip(0).Take(base.DataPerPage).ToList();
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult ManageLazy(int? pageIndex)
        {
            using (DBEntity db = new DBEntity())
            {
                int index = pageIndex ?? 1;
                IList<Module> result = db.Module.OrderBy(m => m.ID).Skip((index - 1) * 3).Take(3).ToList();
                return PartialView("_lazy", result);
            }
        }
    }
}
