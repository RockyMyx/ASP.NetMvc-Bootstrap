using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Data;
using System.Data.Objects;
using System.Web.Mvc;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<Module>, IModuleDao
    {
        public override IEnumerable<Module> GetPagingInfo(int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Take(pageSize).ToList();
            }
        }

        public override IEnumerable<Module> GetPagingInfo(int pageIndex, int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().GetPagingInfo(pageIndex, pageSize).ToList();
            }
        }

        public override IEnumerable<Module> GetEntities(Func<Module, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Where(whereExp).ToList();
            }
        }

        public List<SelectListItem> GetModuleSelect()
        {
            IEnumerable<Module> modules = this.GetAll();
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
