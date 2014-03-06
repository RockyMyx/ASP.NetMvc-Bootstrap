using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class UserRoleDao : BaseEFDao<user_role>, IUserRoleDao
    {
        public UserRoleDao()
        {
            base.PrimaryKey = "UserID";
        }
    }
}
