using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

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
