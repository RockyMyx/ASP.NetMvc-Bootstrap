using System.Collections.Generic;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.Service
{
    public class OperationService : BaseService<T_Operation, IOperationDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new OperationDao();
        }

        public IDictionary<int, string> GetOperations()
        {
            return base.dao.GetOperations();
        }

        public T_Operation GetOperationInfo(FormCollection formInfo)
        {
            T_Operation operation = new T_Operation
            {
                Name = formInfo["Name"].ObjToStr(),
                Action = formInfo["Action"].ObjToStr(),
            };

            return operation;
        }
    }
}
