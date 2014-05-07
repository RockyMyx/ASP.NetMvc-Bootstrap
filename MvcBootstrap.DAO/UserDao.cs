using System.Collections.Generic;
using System.Linq;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.DAO
{
    public class UserDao : BaseEFDao<T_User>, IUserDao
    {
        private IEnumerable<UserViewModel> UserViewModels;

        public UserDao()
        {
            using (DBEntity db = new DBEntity())
            {
                IEnumerable<UserViewModel> models = from u in db.T_User
                                                    join ur in db.T_UserRole
                                                    on u.Id equals ur.UserId
                                                    join r in db.T_Role
                                                    on ur.RoleId equals r.Id
                                                    join un in db.T_UserCache
                                                    on u.Id equals un.UserId
                                                    select new UserViewModel
                                                    {
                                                        UserID = u.Id,
                                                        Name = u.Name,
                                                        Password = u.Password,
                                                        RoleId = r.Id,
                                                        RoleName = r.Name,
                                                        Remark = u.Remark
                                                    };
                UserViewModels = models.ToList();
            }
        }

        public UserLoginViewModel GetUserLoginInfo(string userName, string userPwd)
        {
            using (DBEntity db = new DBEntity())
            {
                //DB操作方法一：使用原生SQL
                //string sql = "select count(*) from User where Name='admin' and Password='admin'";
                //db.ExecuteStoreQuery<int>(sql).FirstOrDefault();
                //db.ExecuteStoreCommand(sql);

                //DB操作方法二：使用参数化SQL
                //string sql = "select count(*) from User where Name=@Name and Password=@Password";
                //var args = new DbParameter[] { new SqlParameter { ParameterName = "Name", Value = "admin" }, new SqlParameter { ParameterName = "Password", Value = "admin" } };
                //db.ExecuteStoreQuery<User>(sql, args);

                //DB操作方法三：使用LINQ
                UserLoginViewModel models = (from u in db.T_User
                                             join r in db.T_UserRole
                                             on u.Id equals r.UserId
                                             where (u.Name == userName && u.Password == userPwd)
                                             select new UserLoginViewModel
                                             {
                                                 UserID = u.Id,
                                                 RoleID = r.RoleId,
                                                 RealName = u.RealName,
                                                 UserName = u.Name
                                             }).FirstOrDefault();
                return models;
            }
        }

        public IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId)
        {
            using (DBEntity db = new DBEntity())
            {
                //ToList立即执行，否则会出错：The ObjectContext instance has been disposed and can no longer be used for operations that require a connection
                return db.GetUserBrowse(roleId).ToList();
            }
        }

        public IEnumerable<string> GetUserOperation(int roleID, int controllerID)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.GetUserOperation(roleID, controllerID).ToList();
            }
        }

        public IEnumerable<UserViewModel> GetAllUserView()
        {
            return UserViewModels.ToList();
        }

        public IEnumerable<UserViewModel> GetPagingUserView(int pageSize)
        {
            return UserViewModels.Take(pageSize).ToList();
        }

        public IEnumerable<UserViewModel> GetSearchPagingUserView(IEnumerable<UserViewModel> entities, int pageIndex, int pageSize)
        {
            using (DBEntity db = new DBEntity())
            {
                return entities.Skip((pageIndex - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();
            }
        }

        public int GetInsertId()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.T_User.Max(u => u.Id);
            }
        }

        public UserViewModel GetUserViewModel(int id)
        {
            return UserViewModels.Where(uv => uv.UserID == id).First();
        }
    }
}
