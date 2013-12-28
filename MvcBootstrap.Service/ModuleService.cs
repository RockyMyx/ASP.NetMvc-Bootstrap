using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Text;

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

        public IList<Module> GetChildModules(int parentId)
        {
            IEnumerable<Module> allModules = base.dao.GetAll();
            IList<Module> childModules = new List<Module>();
            foreach (Module module in allModules)
            {
                if (module.ParentId != null && module.ParentId == parentId)
                {
                    childModules.Add(module);
                }
            }

            return childModules;
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

        public Module GetModuleInfo(FormCollection formInfo)
        {
            Module module = new Module
            {
                ID = formInfo["ID"].ObjToInt(),
                Name = formInfo["Name"].ObjToStr(),
                Code = formInfo["Code"].ObjToStr(),
                Controller = formInfo["Controller"].ObjToStr(),
                IsEnable = formInfo["IsEnable"] == null || string.Compare(formInfo["IsEnable"], "1") == 0
            };
            if (formInfo["ParentId"] != "NULL")
            {
                module.ParentId = formInfo["ParentId"].ObjToInt();
            }

            List<string> operations = formInfo.AllKeys.Where(k => k.Contains("op")).ToList();
            if (operations.Count > 0)
            {
                StringBuilder strOperation = new StringBuilder();
                foreach (string operation in operations)
                {
                    strOperation.Append(operation.Replace("op", "") + ",");
                }

                module.Operations = strOperation.Remove(strOperation.Length - 1, 1).ToString();
            }

            return module;
        }
    }
}
