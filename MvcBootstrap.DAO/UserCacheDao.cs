using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.DAO
{
    public class UserCacheDao : BaseEFDao<T_UserCache>, IUserCacheDao
    {
        public UserCacheDao()
        {
            base.PrimaryKey = "UserId";
        }
    }
}
