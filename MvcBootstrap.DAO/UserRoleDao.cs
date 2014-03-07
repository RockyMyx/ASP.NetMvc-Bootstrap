using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

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
