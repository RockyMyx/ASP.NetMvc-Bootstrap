using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;

namespace MvcBootstrap.MysqlEFModel
{
    public partial class DBEntity : ObjectContext
    {
        DBHelper db = new DBHelper(ConfigurationManager.AppSettings["DBType"]);

        public IEnumerable<T_Module> GetModuleTree()
        {
            using (DBEntity db = new DBEntity())
            {
                return db.ExecuteStoreQuery<T_Module>("CALL GetModuleTree()").ToList();
            }
        }

        public IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId)
        {
            db.AddParameter("RoleID", roleId);
            DbDataReader reader = db.ExecuteReaderByProcedure("GetUserBrowse");
            while (reader.Read())
            {
                yield return new UserBrowseViewModel
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Url = reader["Url"].ToString(),
                    ParentId = Convert.ToInt32(reader["ParentId"])
                };
            }
        }

        public IEnumerable<string> GetUserOperation(int roleId, int controllerId)
        {
            db.AddParameter("RoleID", roleId);
            db.AddParameter("ControllerID", controllerId);
            DbDataReader reader = db.ExecuteReaderByProcedure("GetUserOperation");
            while (reader.Read())
            {
                yield return reader[0].ToString();
            }
        }

        public void DeleteObjects(string ids, string tableName, string primaryKey = "")
        {
            db.AddParameter("ids", ids);
            db.AddParameter("tableName", tableName);
            db.AddParameter("primaryKey", primaryKey);
            db.ExecuteNonQueryByProcedure("DeleteObjects");
        }
    }
}
