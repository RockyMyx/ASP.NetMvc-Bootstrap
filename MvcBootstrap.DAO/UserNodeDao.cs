using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class UserNodeDao : BaseEFDao<user_node>, IUserNodeDao
    {
        public UserNodeDao()
        {
            base.PrimaryKey = "UserID";
        }
    }
}
