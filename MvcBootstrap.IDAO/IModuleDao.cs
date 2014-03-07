using System.Collections.Generic;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.IDAO
{
    public interface IModuleDao : IBaseDao<Module>
    {
        IEnumerable<Module> GetSortedModules();
    }
}
