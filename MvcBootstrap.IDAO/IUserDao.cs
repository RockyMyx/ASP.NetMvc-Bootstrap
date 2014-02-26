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
        IEnumerable<UserViewModel> GetAllUserView();
        IEnumerable<UserViewModel> GetPagingUserView(int pageSize);
        IEnumerable<UserViewModel> GetSearchPagingUserView(IEnumerable<UserViewModel> entities, int pageIndex, int pageSize);
        int GetInsertId();
        UserViewModel GetUserViewModel(int id);
    }
}
