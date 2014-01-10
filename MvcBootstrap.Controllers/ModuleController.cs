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
        ModuleService moduleService = new ModuleService();

        protected override int DataCount
        {
            get { return moduleService.GetEntitiesCount(); }
        }

        public override ActionResult Index()
        {
            ViewData["ParentId"] = moduleService.GetModuleSelect();
            var result = moduleService.GetPagingInfo(base.PageSize);
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            ViewData["ParentId"] = moduleService.GetModuleSelect();
            int index = pageIndex ?? 1;
            //IEnumerable<Module> result = moduleService.GetPagingInfo(index, base.PageSize);
            IEnumerable<Module> searchResult = moduleService.GetSearchModuleCache();
            IEnumerable<Module> result = moduleService.GetSearchModules(searchResult, index, base.PageSize);
            return PartialView("_ModuleGrid", result);
        }

        [HttpPost]
        public override void Update(FormCollection formInfo)
        {
            Module module = moduleService.GetModuleInfo(formInfo);
            moduleService.Update(module);
        }

        [HttpPost]
        public override void Delete(List<int> ids)
        {
            moduleService.Delete(ids);
        }

        [HttpPost]
        public override void Create(FormCollection formInfo)
        {
            Module module = moduleService.GetModuleInfo(formInfo);
            moduleService.Create(module);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<Module> result = moduleService.GetModuleCache().Where(m => m.Name.Contains(name));
            if (result.Count() == 0) return new EmptyResult();
            return PartialView("_ModuleGrid", result);
        }

        public ActionResult Get(int id)
        {
            Module module = moduleService.GetEntity(m => m.ID == id);
            return Json(module, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult AdvanceSearch(FormCollection searchFormInfo)
        {
            Module module = moduleService.GetModuleInfo(searchFormInfo);
            IEnumerable<Module> search = moduleService.GetSortedModules();
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

            IList<Module> result = search.Where(m => m.IsEnable == module.IsEnable).ToList();
            moduleService.GetSearchModuleCache(result, true);
            if (result.Count == 0)
            {
                return new EmptyResult();
            }

            return PartialView("_ModuleGrid", result);
        }
    }
}