using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using System.Web.Mvc;
using MvcBootstrap.IDAO;
using MvcBootstrap.DAO;

namespace MvcBootstrap.Service
{
    public class ModuleService : BaseService<Module, IModuleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new ModuleDao();  
        }

        public int GetControllerIDByName(string controllerName)
        {
            Module module = base.dao.GetEntity(m => m.Controller == controllerName);
            return module.ID;
        }

        public List<SelectListItem> GetModuleSelect()
        {
            IEnumerable<Module> modules = base.dao.GetAll();
            List<SelectListItem> moduleList = new List<SelectListItem>();
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
    }
}
