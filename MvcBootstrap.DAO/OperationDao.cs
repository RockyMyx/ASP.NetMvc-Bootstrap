using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.IDAO;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.DAO
{
    public class OperationDao : BaseEFDao<Operation>, IOperationDao
    {
    }
}
