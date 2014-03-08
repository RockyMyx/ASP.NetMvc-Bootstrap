using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.Service;

namespace MvcBootstrap.Controllers
{
    public class ModuleController : ManageController
    {
        ModuleService moduleService = new ModuleService();

        public ModuleController()
        {
            base.cacheAllKey = "AllModules";
            base.cacheSearchKey = "SearchModules";
            base.SetDetailButton();
        }

        protected override int DataCount
        {
            get { return moduleService.GetEntitiesCount(); }
        }

        public override ActionResult Index()
        {
            Session.Remove(cacheAllKey);
            Session.Remove(cacheSearchKey);
            ViewData["ParentId"] = moduleService.GetModuleSelect();
            var result = moduleService.GetPagingInfo(base.PageSize);
            return View(result);
        }

        public override ActionResult Index(int? pageIndex)
        {
            ViewData["ParentId"] = moduleService.GetModuleSelect();
            int index = pageIndex ?? 1;
            IEnumerable<T_Module> entities = (IEnumerable<T_Module>)Session[cacheSearchKey] ??
                                             moduleService.GetAll();
            IEnumerable<T_Module> result = moduleService.GetSearchPagingInfo(entities, index, base.PageSize);
            return PartialView("_ModuleGrid", result);
        }

        public override void Update(FormCollection formInfo)
        {
            T_Module module = moduleService.GetModuleInfo(formInfo);
            moduleService.Update(module);
        }

        public override void Delete(List<int> ids)
        {
            moduleService.Delete(ids);
        }

        public override void Create(FormCollection formInfo)
        {
            T_Module module = moduleService.GetModuleInfo(formInfo);
            moduleService.Create(module);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<T_Module> filterEntities = moduleService.GetAll().Where(m => m.Name.Contains(name));
            Session[cacheSearchKey] = filterEntities;
            if (filterEntities.Count() == 0) return new EmptyResult();
            return PartialView("_ModuleGrid", filterEntities);
        }

        public ActionResult Get(int id)
        {
            T_Module module = moduleService.GetEntity(m => m.ID == id);
            return Json(module, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult AdvanceSearch(FormCollection searchFormInfo)
        {
            T_Module module = moduleService.GetModuleInfo(searchFormInfo);
            IEnumerable<T_Module> search = moduleService.GetAll().ToList();
            if (!string.IsNullOrEmpty(module.Name))
            {
                search = search.Where(m => m.Name.Contains(module.Name));
            }
            if (!string.IsNullOrEmpty(module.Code))
            {
                search = search.Where(m => m.Code.Contains(module.Code));
            }
            if (!string.IsNullOrEmpty(module.Url))
            {
                search = search.Where(m => m.Url.Contains(module.Url));
            }
            if (module.ParentId != null)
            {
                search = search.Where(m => m.ParentId == module.ParentId);
            }

            IList<T_Module> filterEntities = search.Where(m => m.IsEnable == module.IsEnable).ToList();
            Session[cacheSearchKey] = filterEntities;
            if (filterEntities.Count == 0)
            {
                return new EmptyResult();
            }

            return PartialView("_ModuleGrid", filterEntities);
        }
    }
}