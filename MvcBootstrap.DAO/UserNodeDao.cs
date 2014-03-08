using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.DAO
{
    public class UserNodeDao : BaseEFDao<T_UserNode>, IUserNodeDao
    {
        public UserNodeDao()
        {
            base.PrimaryKey = "UserID";
        }
    }
}
