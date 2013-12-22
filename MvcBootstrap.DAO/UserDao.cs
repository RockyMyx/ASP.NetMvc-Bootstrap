using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Data;

namespace MvcBootstrap.DAO
{
    public class UserDao : BaseEFDao<User>, IUserDao
    {
        public IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId)
        {
            using (DBEntity db = new DBEntity())
            {
                //ToList立即执行，否则会出错：The ObjectContext instance has been disposed and can no longer be used for operations that require a connection
                return db.GetUserBrowse(roleId).ToList();
            }
        }

        public IEnumerable<string> GetUserOperation(int roleID, int controllerID)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetUserOperation(roleID, controllerID).ToList();
            }
        }
    }
}
