using System.Collections.Generic;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.Service
{
    public class OperationService : BaseService<operation, IOperationDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new OperationDao();
        }

        public IDictionary<int, string> GetOperations()
        {
            return base.dao.GetOperations();
        }

        public operation GetOperationInfo(FormCollection formInfo)
        {
            operation operation = new operation
            {
                Name = formInfo["Name"].ObjToStr(),
                Action = formInfo["Action"].ObjToStr(),
            };

            return operation;
        }
    }
}
