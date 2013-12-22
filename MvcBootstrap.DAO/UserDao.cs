using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Data;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.DAO
{
    public class UserDao : BaseEFDao<User>, IUserDao
    {
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
                UserLoginViewModel info = (from u in db.User
                                           join r in db.UserRole
                                           on u.ID equals r.UserID
                                           where (u.Name == userName && u.Password == userPwd)
                                           select new UserLoginViewModel
                                           {
                                               UserID = u.ID,
                                               RoleID = r.RoleID,
                                               RealName = u.RealName,
                                               UserName = u.Name
                                           }).FirstOrDefault();
                return info;
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
    }
}
