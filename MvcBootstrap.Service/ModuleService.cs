using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Text;
using System;
using System.Web.Caching;
using System.Web;

namespace MvcBootstrap.Service
{
    public class ModuleService : BaseService<Module, IModuleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new ModuleDao();
        }

        private IEnumerable<Module> CacheModules
        {
            get { return this.GetAll(); }
        }

        public IEnumerable<Module> GetSortedModules()
        {
            return base.dao.GetSortedModules();
        }

        public int GetModuleParentId(int moduleId)
        {
            return Convert.ToInt32(CacheModules.Where(m => m.ID == moduleId)
                                               .Select(m => m.ParentId)
                                               .Single());
        }

        public int GetModuleIdByName(string controllerName)
        {
            return CacheModules.Where(m => m.Controller == controllerName).Single().ID;
        }

        public IList<Module> GetChildModules(int parentId)
        {
            IList<Module> childModules = new List<Module>();
            CacheModules.Enumerate(m => m.ParentId != null && m.ParentId == parentId,
                                   m => childModules.Add(m));
            return childModules;
        }

        public IList<Module> GetParentModules()
        {
            IList<Module> parentModules = new List<Module>();
            CacheModules.Enumerate(m => m.ParentId == null,
                                   m => parentModules.Add(m));
            return parentModules;
        }

        public IList<SelectListItem> GetModuleSelect()
        {
            IList<SelectListItem> moduleList = new List<SelectListItem>();
            moduleList.Add(new SelectListItem { Text = "请选择", Value = "NULL" });
            CacheModules.Enumerate(m => m.ParentId == null,
                                   m => moduleList.Add(new SelectListItem
                                   {
                                       Text = m.Name,
                                       Value = m.ID.ToString()
                                   }));
            return moduleList;
        }

        public Module GetModuleInfo(FormCollection formInfo)
        {
            int id = formInfo["ID"].ObjToInt();
            Module oriModule = dao.GetEntity(m => m.ID == id);
            Module module = new Module
            {
                ID = id,
                Name = formInfo["Name"] == null ? oriModule.Name : formInfo["Name"],
                Code = formInfo["Code"] == null ? oriModule.Code : formInfo["Code"],
                Url = formInfo["Url"] == null ? oriModule.Url : formInfo["Url"],
                Controller = formInfo["Controller"] == null ? oriModule.Controller : formInfo["Controller"],
                ParentId = formInfo["ParentId"] == "NULL" ? oriModule.ParentId : Convert.ToInt32(formInfo["ParentId"]),
                IsEnable = string.Compare(formInfo["IsEnable"], "1") == 0
            };

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
