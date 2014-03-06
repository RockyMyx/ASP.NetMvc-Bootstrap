using System.Collections.Generic;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.IDAO
{
    public interface IOperationDao : IBaseDao<operation>
    {
        IDictionary<int, string> GetOperations();
    }
}
