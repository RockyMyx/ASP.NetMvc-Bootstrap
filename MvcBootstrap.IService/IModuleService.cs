using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using System.Web.Mvc;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.IService
{
    public interface IModuleService : IBaseService<Module, IModuleDao>
    {
        int GetControllerIDByName(string controllerName);
        List<SelectListItem> GetModuleSelect();
    }
}
