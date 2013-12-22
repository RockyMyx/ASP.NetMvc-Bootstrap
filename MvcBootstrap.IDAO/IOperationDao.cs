using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.IDAO
{
    public interface IOperationDao : IBaseDao<Operation>
    {
        IDictionary<int, string> GetOperations();
    }
}
