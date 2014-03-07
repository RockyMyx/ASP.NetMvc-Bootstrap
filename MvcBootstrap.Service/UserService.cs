using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;
using MvcBootstrap.ViewModels;

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

        public IList<SelectListItem> GetRoleSelect()
        {
            RoleService roleService = new RoleService();
            IEnumerable<Role> roles = roleService.GetAll();

            IList<SelectListItem> roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem { Text = "请选择", Value = "NULL" });
            roles.Enumerate(m => roleList.Add(new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.ID.ToString()
                            }));
            return roleList;
        }

        public User GetUserInfo(FormCollection formInfo)
        {
            User user = new User
            {
                ID = Convert.ToInt32(formInfo["ID"]),
                Name = formInfo["Name"],
                Password = formInfo["Password"],
                Remark = formInfo["Remark"]
            };

            return user;
        }

        public UserRole GetNewUserRoleInfo(FormCollection formInfo)
        {
            UserRole ur = new UserRole
            {
                UserID = dao.GetInsertId(),
                RoleID = Convert.ToInt32(formInfo["RoleID"])
            };

            return ur;
        }

        public UserNode GetNewUserNodeInfo(FormCollection formInfo)
        {
            UserNode un = new UserNode
            {
                UserID = dao.GetInsertId()
            };

            return un;
        }

        public UserRole GetEditUserRoleInfo(FormCollection formInfo)
        {
            UserRole ur = new UserRole
            {
                UserID = Convert.ToInt32(formInfo["ID"]),
                RoleID = Convert.ToInt32(formInfo["RoleID"])
            };

            return ur;
        }

        public UserViewModel GetUserViewModel(int id)
        {
            return base.dao.GetUserViewModel(id);
        }
    }
}
