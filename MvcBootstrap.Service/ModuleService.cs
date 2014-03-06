using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.Service
{
    public class ModuleService : BaseService<module, IModuleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new ModuleDao();
        }

        private IEnumerable<module> CacheModules
        {
            get { return this.GetAll(); }
        }

        public IEnumerable<module> GetSortedModules()
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

        public IList<module> GetChildModules(int parentId)
        {
            IList<module> childModules = new List<module>();
            CacheModules.Enumerate(m => m.ParentId != null && m.ParentId == parentId,
                                   m => childModules.Add(m));
            return childModules;
        }

        public IList<module> GetParentModules()
        {
            IList<module> parentModules = new List<module>();
            CacheModules.Enumerate(m => m.ParentId == 0,
                                   m => parentModules.Add(m));
            return parentModules;
        }

        public IList<SelectListItem> GetModuleSelect()
        {
            IList<SelectListItem> moduleList = new List<SelectListItem>();
            moduleList.Add(new SelectListItem { Text = "请选择", Value = "NULL" });
            CacheModules.Enumerate(m => m.ParentId == 0,
                                   m => moduleList.Add(new SelectListItem
                                   {
                                       Text = m.Name,
                                       Value = m.ID.ToString()
                                   }));
            return moduleList;
        }

        public module GetModuleInfo(FormCollection formInfo)
        {
            int id = formInfo["ID"].ObjToInt();
            module oriModule = dao.GetEntity(m => m.ID == id);
            module module = new module
            {
                ID = id,
                Name = formInfo["Name"] == null ? oriModule.Name : formInfo["Name"],
                Code = formInfo["Code"] == null ? oriModule.Code : formInfo["Code"],
                Url = formInfo["Url"] == null ? oriModule.Url : formInfo["Url"],
                Controller = formInfo["Controller"] == null ? oriModule.Controller : formInfo["Controller"],
                ParentId = Convert.ToInt32(formInfo["ParentId"]),
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
