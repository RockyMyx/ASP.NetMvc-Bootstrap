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
        public ModuleService()
        {
            base.dao = new ModuleDao();   
        }

        public List<SelectListItem> GetModuleSelect()
        {
            return base.dao.GetModuleSelect();
        }
    }
}
