using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.Service
{
    public class UserNodeService : BaseService<user_node, IUserNodeDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserNodeDao();
        }
    }
}
