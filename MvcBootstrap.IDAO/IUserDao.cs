using System.Collections.Generic;
using MvcBootstrap.EFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.IDAO
{
    public interface IUserDao : IBaseDao<User>
    {
        UserLoginViewModel GetUserLoginInfo(string userName, string userPwd);
        IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId);
        IEnumerable<string> GetUserOperation(int roleID, int controllerID);
    }
}
