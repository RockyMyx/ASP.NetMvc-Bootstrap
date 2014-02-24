using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class UserRoleService : BaseService<UserRole, IUserRoleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserRoleDao();
        }
    }
}
