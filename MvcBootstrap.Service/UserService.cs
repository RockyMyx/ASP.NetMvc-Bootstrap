using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Service
{
    public class UserService : BaseService<T_User, IUserDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserDao();
        }

        public UserLoginViewModel GetUserLoginInfo(string userName, string userPwd)
        {
            return base.dao.GetUserLoginInfo(userName, userPwd);
        }

        public bool IsUserExist(string userName)
        {
            return Convert.ToBoolean(base.dao.GetEntitiesCount(u => u.Name == userName));
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
            IEnumerable<T_Role> roles = roleService.GetAll();

            IList<SelectListItem> roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem { Text = "请选择", Value = "NULL" });
            roles.Enumerate(m => roleList.Add(new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }));
            return roleList;
        }

        public T_User GetUserInfo(FormCollection formInfo)
        {
            T_User user = new T_User
            {
                Id = Convert.ToInt32(formInfo["ID"]),
                Name = formInfo["Name"],
                Password = formInfo["Password"],
                Remark = formInfo["Remark"]
            };

            return user;
        }

        public T_UserRole GetNewUserRoleInfo(FormCollection formInfo)
        {
            T_UserRole ur = new T_UserRole
            {
                UserId = dao.GetInsertId(),
                RoleId = Convert.ToInt32(formInfo["RoleID"])
            };

            return ur;
        }

        public T_UserCache GetNewUserNodeInfo(FormCollection formInfo)
        {
            T_UserCache un = new T_UserCache
            {
                UserId = dao.GetInsertId()
            };

            return un;
        }

        public T_UserRole GetEditUserRoleInfo(FormCollection formInfo)
        {
            T_UserRole ur = new T_UserRole
            {
                UserId = Convert.ToInt32(formInfo["ID"]),
                RoleId = Convert.ToInt32(formInfo["RoleID"])
            };

            return ur;
        }

        public UserViewModel GetUserViewModel(int id)
        {
            return base.dao.GetUserViewModel(id);
        }
    }
}
