using System.Collections.Generic;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.IDAO
{
    public interface IUserDao : IBaseDao<T_User>
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
