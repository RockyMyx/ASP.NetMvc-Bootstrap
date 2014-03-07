using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class UserNodeDao : BaseEFDao<UserNode>, IUserNodeDao
    {
        public UserNodeDao()
        {
            base.PrimaryKey = "UserID";
        }
    }
}
