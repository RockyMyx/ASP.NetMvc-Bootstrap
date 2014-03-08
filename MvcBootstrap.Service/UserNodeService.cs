using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.Service
{
    public class UserNodeService : BaseService<T_UserNode, IUserNodeDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserNodeDao();
        }
    }
}
