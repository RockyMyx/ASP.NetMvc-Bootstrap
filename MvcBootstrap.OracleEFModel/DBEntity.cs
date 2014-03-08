using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;

namespace MvcBootstrap.OracleEFModel
{
    public partial class DBEntity : ObjectContext
    {
        DBHelper db = new DBHelper(ConfigurationManager.AppSettings["DBType"]);

        public IEnumerable<T_Module> GetModuleTree()
        {
            using (DBEntity db = new DBEntity())
            {
                //return db.ExecuteStoreQuery<T_Module>("CALL GetModuleTree()").ToList();
                return db.T_Module.ToList();
            }
        }

        public IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId)
        {
            /*string sql = @"SELECT    m.ID, m.Name, m.Code, m.Url, m.ParentId
                           FROM      T_Module m INNER JOIN
                                     T_Permission p ON m.ID = p.ControllerID INNER JOIN
                                     T_Role r ON p.RoleID = r.ID
                           WHERE     (p.RoleID = {0}RoleID) AND 
                                     (p.ActionID = 1)     AND 
                                     (m.IsEnable = 1)     AND 
                                     (r.IsEnable = 1)";
            db.AddParameter("RoleID", roleId);
            DbDataReader reader = db.ExecuteReaderWithParams(sql);*/
            string sql = string.Format(@"SELECT    m.ID, m.Name, m.Code, m.Url, m.ParentId
                                         FROM      T_Module m INNER JOIN
                                                   T_Permission p ON m.ID = p.ControllerID INNER JOIN
                                                   T_Role r ON p.RoleID = r.ID
                                         WHERE     (p.RoleID = {0}) AND 
                                                   (p.ActionID = 1)     AND 
                                                   (m.IsEnable = 1)     AND 
                                                   (r.IsEnable = 1)", roleId);
            DbDataReader reader = db.ExecuteReader(sql);
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
            /*string sql = @"SELECT o.Action FROM T_Permission p
	                       INNER JOIN T_Operation o ON o.ID = p.ActionID
	                       INNER JOIN T_Role r ON p.RoleID = r.ID
	                       INNER JOIN T_Module m ON p.ControllerID = m.ID
	                       WHERE p.RoleID = {0}RoleID AND
	                             p.ControllerID = {0}ControllerID AND
	                             p.ActionID != 1 AND
	                             r.IsEnable = 1 AND
	                             m.IsEnable = 1";
            db.AddParameter("RoleID", roleId);
            db.AddParameter("ControllerID", controllerId);
            DbDataReader reader = db.ExecuteReaderWithParams(sql);*/
            string sql = string.Format(@"SELECT o.Action FROM T_Permission p
	                       INNER JOIN T_Operation o ON o.ID = p.ActionID
	                       INNER JOIN T_Role r ON p.RoleID = r.ID
	                       INNER JOIN T_Module m ON p.ControllerID = m.ID
	                       WHERE p.RoleID = {0} AND
	                             p.ControllerID = {1} AND
	                             p.ActionID != 1 AND
	                             r.IsEnable = 1 AND
	                             m.IsEnable = 1", roleId, controllerId);
            DbDataReader reader = db.ExecuteReader(sql);
            while (reader.Read())
            {
                yield return reader[0].ToString();
            }
        }

        public void DeleteObjects(string ids, string tableName, string primaryKey = "")
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1} in ({2})",
                                        tableName,
                                        primaryKey,
                                        ids);
            db.ExecuteNonQuery(sql);

            /*string sql = "DELETE FROM {0}Table WHERE {0}Key in ({0}ids)";
            db.AddParameter("Table", tableName);
            db.AddParameter("Key", primaryKey);
            db.AddParameter("ids", ids);
            db.ExecuteNonQueryWithParams(sql);*/
        }
    }
}
