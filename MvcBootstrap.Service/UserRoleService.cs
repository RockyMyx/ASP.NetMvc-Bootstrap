using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.Service
{
    public class UserRoleService : BaseService<T_UserRole, IUserRoleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserRoleDao();
        }
    }
}
