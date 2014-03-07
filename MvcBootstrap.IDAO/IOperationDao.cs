using System.Collections.Generic;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.IDAO
{
    public interface IOperationDao : IBaseDao<Operation>
    {
        IDictionary<int, string> GetOperations();
    }
}
