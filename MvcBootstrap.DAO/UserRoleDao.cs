using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

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
