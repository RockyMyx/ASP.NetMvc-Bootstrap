using System.Collections.Generic;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.IDAO
{
    public interface IOperationDao : IBaseDao<T_Operation>
    {
        IDictionary<int, string> GetOperations();
    }
}
