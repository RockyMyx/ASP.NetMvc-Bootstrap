using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using System.Web.Mvc;

namespace MvcBootstrap.IBLL
{
    public interface IModuleService : IBaseService<Module>
    {
        List<SelectListItem> GetModuleSelect();
    }
}
