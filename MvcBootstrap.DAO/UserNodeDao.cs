using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.DAO
{
    public class UserNodeDao : BaseEFDao<T_UserNode>, IUserNodeDao
    {
        public UserNodeDao()
        {
            base.PrimaryKey = "UserID";
        }
    }
}
