using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Data;
using System.Data.Objects;

namespace MvcBootstrap.DAO
{
    public class ModuleDao : BaseEFDao<Module>, IModuleDao<Module>
    {
    }
}
