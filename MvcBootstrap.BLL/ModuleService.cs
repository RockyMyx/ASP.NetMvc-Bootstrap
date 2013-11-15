using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using System.Web.Mvc;
using MvcBootstrap.DAO;

namespace MvcBootstrap.BLL
{
    public class ModuleService : BaseService<Module>
    {
        public ModuleDao dao = null;
        public ModuleService()
        {
            dao = new ModuleDao();
        }

        public List<SelectListItem> GetModuleSelect()
        {
            IList<Module> modules = dao.GetAll();
            List<SelectListItem> moduleList = new List<SelectListItem>();
            int isParent;
            moduleList.Add(new SelectListItem { Text = "请选择", Value = "NULL" });
            for (int i = 0; i < modules.Count; i++)
            {
                if (!int.TryParse(modules[i].ParentId.ToString(), out isParent))
                {
                    moduleList.Add(new SelectListItem 
                    { 
                        Text = modules[i].Name, 
                        Value = modules[i].ID.ToString() 
                    });
                }
            }

            return moduleList;
        }
    }
}
