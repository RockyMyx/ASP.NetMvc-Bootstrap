using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.IDAO
{
    public interface IUserDao : IBaseDao<User>
    {
        IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId);
    }
}
