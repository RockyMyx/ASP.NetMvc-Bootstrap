using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using MvcBootstrap.Service;

namespace MvcBootstrap.Controllers
{
    public class ModuleController : ManageController
    {
        ModuleService service = new ModuleService();

        protected override int DataCount
        {
            get { return service.GetEntitiesCount(); }
        }

        public override ActionResult Index()
        {
            ViewData["ParentId"] = service.GetModuleSelect();
            var result = service.GetPagingInfo(base.PageSize);
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            ViewData["ParentId"] = service.GetModuleSelect();
            int index = pageIndex ?? 1;
            IEnumerable<Module> result = service.GetPagingInfo(index, base.PageSize);
            return PartialView("_ModuleGrid", result);
        }

        [HttpPost]
        public override void Update(FormCollection formInfo)
        {
            Module module = FormHelper.GetModuleInfo(formInfo);
            service.Update(module);
        }

        [HttpPost]
        public override void Delete(List<int> ids)
        {
            service.Delete(ids);
        }

        [HttpPost]
        public override void Create(FormCollection formInfo)
        {
            Module module = FormHelper.GetModuleInfo(formInfo);
            service.Create(module);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<Module> result = service.GetEntities(m => m.Name.Contains(name));
            if (result.Count() == 0) return new EmptyResult();
            return PartialView("_ModuleGrid", result);
        }

        public ActionResult Get(int id)
        {
            Module module = service.GetEntity(m => m.ID == id);
            return Json(module, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public override ActionResult AdvanceSearch(FormCollection searchFormInfo)
        {
            Module module = FormHelper.GetModuleInfo(searchFormInfo);
            IQueryable<Module> search = service.GetSortedModules();
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
    }
}