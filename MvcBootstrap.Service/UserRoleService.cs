using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.Service
{
    public class UserRoleService : BaseService<user_role, IUserRoleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserRoleDao();
        }
    }
}
