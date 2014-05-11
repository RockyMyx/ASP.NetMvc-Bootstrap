using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.Service
{
    public class ModuleService : BaseService<T_Module, IModuleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new ModuleDao();
        }

        private IEnumerable<T_Module> CacheModules
        {
            get { return this.GetAll(); }
        }

        public IEnumerable<T_Module> GetSortedModules()
        {
            return base.dao.GetSortedModules();
        }

        public int GetModuleParentId(int moduleId)
        {
            return Convert.ToInt32(CacheModules.Where(m => m.Id == moduleId)
                                               .Select(m => m.ParentId)
                                               .Single());
        }

        public int GetModuleIdByName(string controllerName)
        {
            return CacheModules.Where(m => m.Controller == controllerName).Single().Id;
        }

        public IList<T_Module> GetChildModules(int parentId)
        {
            IList<T_Module> childModules = new List<T_Module>();
            CacheModules.Enumerate(m => m.ParentId != null && m.ParentId == parentId,
                                   m => childModules.Add(m));
            return childModules;
        }

        public IList<T_Module> GetParentModules()
        {
            IList<T_Module> parentModules = new List<T_Module>();
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
                                       Value = m.Id.ToString()
                                   }));
            return moduleList;
        }

        public T_Module GetModuleInfo(FormCollection formInfo)
        {
            int id = formInfo["ID"].ObjToInt();
            T_Module oriModule = dao.GetEntity(m => m.Id == id);
            T_Module module = new T_Module
            {
                Id = id,
                Name = formInfo["Name"] == null ? oriModule.Name : formInfo["Name"],
                Code = formInfo["Code"] == null ? oriModule.Code : formInfo["Code"],
                Url = formInfo["Url"] == null ? oriModule.Url : formInfo["Url"],
                Controller = formInfo["Controller"] == null ? oriModule.Controller : formInfo["Controller"],
                ParentId = Convert.ToInt32(formInfo["ParentId"]),
                IsEnable = string.Compare(formInfo["IsEnable"], "1") == 0
                //IsEnable = short.Parse(formInfo["IsEnable"])
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
