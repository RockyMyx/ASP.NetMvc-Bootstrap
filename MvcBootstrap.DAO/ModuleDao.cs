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

        public override IEnumerable<Module> GetEntities(Func<Module, bool> whereExp)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetModuleTree().Where(whereExp);
            }
        }

        public List<SelectListItem> GetModuleSelect()
        {
            IList<Module> modules = this.GetAll();
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
