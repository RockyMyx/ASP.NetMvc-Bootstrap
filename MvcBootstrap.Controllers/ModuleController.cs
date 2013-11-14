using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using System.Text;
using System.Data;
using System.Data.Objects;

namespace MvcBootstrap.Controllers
{
    public class ModuleController : ManageController
    {
        protected override int DataCount
        {
            get { return db.Module.Count(); }
        }

        public override ActionResult Index()
        {
            List<Module> modules = db.Module.ToList();
            List<SelectListItem> moduleList = new List<SelectListItem>();
            int isParent;
            moduleList.Add(new SelectListItem { Text = "请选择", Value = "NULL" });
            for (int i = 0; i < modules.Count; i++)
            {
                if (!int.TryParse(modules[i].ParentId.ToString(), out isParent))
                {
                    moduleList.Add(new SelectListItem { Text = modules[i].Name, Value = modules[i].ID.ToString() });
                }
            }

            ViewData["ParentId"] = moduleList;
            var result = db.GetModuleTree().Take(base.PageSize).ToList();
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IEnumerable<Module> result = db.GetModuleTree().GetPagingInfo(index, base.PageSize);
            return PartialView("_ModuleGrid", result);
        }

        [HttpPost]
        public override void Update(FormCollection formInfo)
        {
            Module module = FormHelper.GetModuleInfo(formInfo);
            //Module oldModule = db.Module.Where(m => m.ID == id).Single();
            //oldModule.ID = id;
            //oldModule.Name = module.Name;
            //oldModule.Controller = module.Controller;
            //oldModule.IsEnable = module.IsEnable;
            //oldModule.Operations = module.Operations;
            //db.SaveChanges();

            try
            {
                var obj = db.CreateObjectSet<Module>();
                obj.Attach(module);
                db.ObjectStateManager.ChangeObjectState(module, EntityState.Modified);
                db.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                db.Refresh(RefreshMode.StoreWins, module);
            }
        }

        [HttpPost]
        public override void Delete(List<int> ids)
        {
            Module module = null;
            foreach (int id in ids)
            {
                module = db.Module.GetEntity(m => m.ID == id);
                db.DeleteObject(module);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public override void Create(FormCollection formInfo)
        {
            Module module = FormHelper.GetModuleInfo(formInfo);
            db.Module.AddObject(module);
            db.SaveChanges();
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IList<Module> result = db.GetModuleTree().Where(m => m.Name.Contains(name)).ToList();
            if (result.Count() == 0)
            {
                return new EmptyResult();
            }
            return PartialView("_ModuleGrid", result);
        }

        public ActionResult Get(int id)
        {
            using (DBEntity db = new DBEntity())
            {
                Module module = db.Module.Where(m => m.ID == id).Single();
                return Json(module, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public override ActionResult AdvanceSearch(FormCollection searchFormInfo)
        {
            try
            {
                Module module = FormHelper.GetModuleInfo(searchFormInfo);
                IQueryable<Module> search = db.GetModuleTree().AsQueryable();
                if (!string.IsNullOrEmpty(module.Name))
                {
                    search = search.Where(m => m.Name.Contains(module.Name));
                }
                if (!string.IsNullOrEmpty(module.Code))
                {
                    search = search.Where(m => m.Name.Contains(module.Code));
                }
                if (!string.IsNullOrEmpty(module.Controller))
                {
                    search = search.Where(m => m.Name.Contains(module.Controller));
                }
                if (module.ParentId != null)
                {
                    search = search.Where(m => m.ParentId == module.ParentId);
                }

                List<Module> result = search.Where(m => m.IsEnable == module.IsEnable).ToList();
                if (result.Count == 0)
                {
                    return new EmptyResult();
                }

                return PartialView("_ModuleGrid", result);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}