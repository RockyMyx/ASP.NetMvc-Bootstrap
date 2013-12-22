using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.DAO
{
    public class OperationDao : BaseEFDao<Operation>, IOperationDao
    {
        public IEnumerable<string> GetUserOperation(int roleID, int controllerID)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetUserOperation(roleID, controllerID);
            }
        }
    }
}
