using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.DAO
{
    public class UserRoleDao : BaseEFDao<T_UserRole>, IUserRoleDao
    {
        public UserRoleDao()
        {
            base.PrimaryKey = "UserID";
        }
    }
}
