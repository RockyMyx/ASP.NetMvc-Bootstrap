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
                return db.GetUserBrowse(roleId).AsEnumerable();
            }
        }

        public IEnumerable<string> GetUserOperation(int roleID, int controllerID)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetUserOperation(roleID, controllerID);
            }
        }
    }
}
