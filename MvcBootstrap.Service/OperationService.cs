using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class OperationService : BaseService<Operation, IOperationDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new OperationDao();
        }

        public IDictionary<int, string> GetOperations()
        {
            return base.dao.GetOperations();
        }
    }
}
