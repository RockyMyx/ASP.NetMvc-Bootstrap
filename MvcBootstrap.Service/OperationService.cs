using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Web.Mvc;

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

        public Operation GetOperationInfo(FormCollection formInfo)
        {
            Operation operation = new Operation
            {
                Name = formInfo["Name"].ObjToStr(),
                Action = formInfo["Action"].ObjToStr(),
            };

            return operation;
        }
    }
}
