using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.IService
{
    public interface IUserService : IBaseService<User, IUserDao>
    {
        IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId);
        IEnumerable<string> GetUserOperation(int roleID, int controllerID);
    }
}
