using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.IService
{
    public interface IOperationService : IBaseService<Operation, IOperationDao>
    {
        IEnumerable<string> GetUserOperation(int roleID, int controllerID);
    }
}
