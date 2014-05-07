using System.Collections.Generic;
using System.Linq;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.DAO
{
    public class OperationDao : BaseEFDao<T_Operation>, IOperationDao
    {
        public IDictionary<int, string> GetOperations()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.T_Operation
                         .Select(s => new { s.Id, s.Name })
                         .AsEnumerable()
                         .ToDictionary(k => k.Id, k => k.Name);
            }
        }
    }
}
