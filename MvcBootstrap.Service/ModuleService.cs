using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class ModuleService : BaseService<Module, IModuleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new ModuleDao();  
        }

        public IQueryable<Module> GetSortedModules()
        {
            return base.dao.GetSortedModules();
        }

        public int GetModuleIdByName(string controllerName)
        {
            Module module = base.dao.GetEntity(m => m.Controller == controllerName);
            return module.ID;
        }

        public int GetModuleParentId(int moduleId)
        {
            return base.dao.GetModuleParentId(moduleId);
        }

        public IList<SelectListItem> GetModuleSelect()
        {
            IEnumerable<Module> modules = base.dao.GetAll();
            IList<SelectListItem> moduleList = new List<SelectListItem>();
            int isParent;
            moduleList.Add(new SelectListItem { Text = "请选择", Value = "NULL" });
            foreach (Module module in modules)
            {
                if (!int.TryParse(module.ParentId.ToString(), out isParent))
                {
                    moduleList.Add(new SelectListItem
                    {
                        Text = module.Name,
                        Value = module.ID.ToString()
                    });
                }
            }

            return moduleList;
        }

        public IList<Module> GetParentModules()
        {
            IEnumerable<Module> allModules = base.dao.GetAll().ToList();
            IList<Module> parentModules = new List<Module>();
            foreach (Module module in allModules)
            {
                if (module.ParentId == null)
                {
                    parentModules.Add(module);
                }
            }

            return parentModules;
        }
    }
}
