using System.Linq;
using MvcBootstrap.EFModel;
using System.Collections.Generic;

namespace MvcBootstrap.IDAO
{
    public interface IModuleDao : IBaseDao<Module>
    {
        IEnumerable<Module> GetSortedModules();
    }
}
