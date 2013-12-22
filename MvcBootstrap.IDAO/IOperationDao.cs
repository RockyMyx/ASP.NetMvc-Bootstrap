using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.IDAO
{
    public interface IOperationDao : IBaseDao<Operation>
    {
        IEnumerable<string> GetUserOperation(int roleID, int controllerID);
    }
}
