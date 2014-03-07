using System.Collections.Generic;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.IDAO
{
    public interface IModuleDao : IBaseDao<T_Module>
    {
        IEnumerable<T_Module> GetSortedModules();
    }
}
