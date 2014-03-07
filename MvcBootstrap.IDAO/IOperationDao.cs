using System.Collections.Generic;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.IDAO
{
    public interface IOperationDao : IBaseDao<T_Operation>
    {
        IDictionary<int, string> GetOperations();
    }
}
