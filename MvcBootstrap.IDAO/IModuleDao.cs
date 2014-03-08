using System.Collections.Generic;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.IDAO
{
    public interface IModuleDao : IBaseDao<T_Module>
    {
        IEnumerable<T_Module> GetSortedModules();
    }
}
