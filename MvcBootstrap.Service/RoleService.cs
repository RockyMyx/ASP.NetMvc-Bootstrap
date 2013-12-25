using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class RoleService : BaseService<Role, IRoleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new RoleDao();
        }
    }
}