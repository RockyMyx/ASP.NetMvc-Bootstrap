using System.Collections.Generic;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.IDAO
{
    public interface IOperationDao : IBaseDao<Operation>
    {
        IDictionary<int, string> GetOperations();
    }
}
