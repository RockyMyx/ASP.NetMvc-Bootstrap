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
                                Value = m.ID.ToString()
                            }));
            return roleList;
        }

        public T_User GetUserInfo(FormCollection formInfo)
        {
            T_User user = new T_User
            {
                ID = Convert.ToInt32(formInfo["ID"]),
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
                UserID = dao.GetInsertId(),
                RoleID = Convert.ToInt32(formInfo["RoleID"])
            };

            return ur;
        }

        public T_UserNode GetNewUserNodeInfo(FormCollection formInfo)
        {
            T_UserNode un = new T_UserNode
            {
                UserID = dao.GetInsertId(),
                CanAddRootNode = formInfo["CanAddRootNode"] == null ? false : true,
                CanAddChildNode = formInfo["CanAddChildNode"] == null ? false : true,
                CanRenameNode = formInfo["CanRenameNode"] == null ? false : true,
                CanDeleteNode = formInfo["CanDeleteNode"] == null ? false : true,
                CanAddResource = formInfo["CanAddResource"] == null ? false : true,
                CanUpdateResource = formInfo["CanUpdateResource"] == null ? false : true,
                CanDeleteResource = formInfo["CanDeleteResource"] == null ? false : true,
            };

            return un;
        }

        public T_UserRole GetEditUserRoleInfo(FormCollection formInfo)
        {
            T_UserRole ur = new T_UserRole
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
