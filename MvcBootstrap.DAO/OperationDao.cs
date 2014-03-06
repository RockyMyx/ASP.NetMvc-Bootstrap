using System.Collections.Generic;
using System.Linq;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class OperationDao : BaseEFDao<operation>, IOperationDao
    {
        public IDictionary<int, string> GetOperations()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.operation
                         .Select(s => new { s.ID, s.Name })
                         .AsEnumerable()
                         .ToDictionary(k => k.ID, k => k.Name);
            }
        }
    }
}
