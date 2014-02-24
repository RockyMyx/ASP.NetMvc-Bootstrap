using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.ViewModels;
using System.Web.Mvc;

namespace MvcBootstrap.Service
{
    public class UserService : BaseService<User, IUserDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserDao();
        }

        public UserLoginViewModel GetUserLoginInfo(string userName, string userPwd)
        {
            return base.dao.GetUserLoginInfo(userName, userPwd);
        }

        public IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId)
        {
            return base.dao.GetUserBrowse(roleId);
        }

        public IEnumerable<string> GetUserOperation(int roleID, int controllerID)
        {
            return base.dao.GetUserOperation(roleID, controllerID);
        }

        public IEnumerable<UserViewModel> GetAllUserView()
        {
            return base.dao.GetAllUserView();
        }

        public IEnumerable<UserViewModel> GetPagingUserView(int pageSize)
        {
            return base.dao.GetPagingUserView(pageSize);
        }

        public IEnumerable<UserViewModel> GetSearchPagingUserView(IEnumerable<UserViewModel> entities, int pageIndex, int pageSize)
        {
            return base.dao.GetSearchPagingUserView(entities, pageIndex, pageSize);
        }

        public User GetUserInfo(FormCollection formInfo)
        {
            User role = new User
            {
                Name = formInfo["Name"],
                RealName = formInfo["RealName"],
                Remark = formInfo["Remark"]
            };

            return role;
        }
    }
}
