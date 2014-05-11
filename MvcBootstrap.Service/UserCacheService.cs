using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.Service
{
    public class UserCacheService : BaseService<T_UserCache, IUserCacheDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserCacheDao();
        }
    }
}
