using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class UserRoleDao : BaseEFDao<UserRole>, IUserRoleDao
    {
        public UserRoleDao()
        {
            base.PrimaryKey = "UserID";
        }
    }
}
