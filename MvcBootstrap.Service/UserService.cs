using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.ViewModels;
using System.Web.Mvc;
using System;

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
            User role = new User
            {
                Name = formInfo["Name"],
                Password = formInfo["Password"],
                Remark = formInfo["Remark"]
            };

            return role;
        }

        public UserRole GetUserRoleInfo(FormCollection formInfo)
        {
            UserRole ur = new UserRole
            {
                UserID = dao.GetInsertId(),
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
