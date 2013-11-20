using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using System.Web.Mvc;

namespace MvcBootstrap.IDAO
{
    public interface IModuleDao : IBaseDao<Module>
    {
        List<SelectListItem> GetModuleSelect();
    }
}
