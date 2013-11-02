using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;
using MvcBootstrapManage.ViewModel;

namespace MvcBootstrapManage.Controllers
{
    public class ModuleController : BaseController
    {
        public ModuleController()
        {
            base.RecordCount = db.Module.Count();
        }

        public ActionResult Index()
        {
            using (DBEntity db = new DBEntity())
            {
                ViewBag.TotalCount = base.RecordCount;
                var result = db.GetModuleTree().Take(base.DataPerPage).ToList();
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult Index(int? pageIndex)
        {
            using (DBEntity db = new DBEntity())
            {
                int index = pageIndex ?? 1;
                IList<Module> result = db.GetModuleTree().Skip((index - 1) * 3).Take(base.DataPerPage).ToList();
                return PartialView("_ModuleGrid", result);
            }
        }

        [HttpPost]
        public ActionResult Update(ModuleEditViewModel viewModel)
        {
            Module module = db.Module.Where(m => m.ID == viewModel.ID).Single();
            module.Url = viewModel.Url;
            module.IsVisible = int.Parse(viewModel.IsVisible) == 1 ? true : false;
            db.SaveChanges();
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(List<int> ids)
        {
            foreach (int id in ids)
            {
                Module module = db.Module.Where(m => m.ID == id).Single();
                db.DeleteObject(module);
            }
            db.SaveChanges();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Add(FormCollection formInfo)
        {
            Module module = new Module();
            module.Name = formInfo["Name"].ToString();
            module.Code = formInfo["Code"].ToString();
            module.ParentId = Convert.ToInt32(formInfo["ParentId"]);
            module.IsVisible = string.Compare(formInfo["IsVisible"], "1") == 0;
            db.Module.AddObject(module);
            db.SaveChanges();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Search(string name)
        {
            using (DBEntity db = new DBEntity())
            {
                IList<Module> result = db.Module.Where(m => m.Name.Contains(name)).OrderBy(m => m.ID).ToList();
                ViewBag.SearchCount = result.Count;
                return PartialView("_ModuleGrid", result);
            }
        }
    }
}