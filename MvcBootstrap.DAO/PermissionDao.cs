using System.Collections.Generic;
using System.Linq;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.DAO
{
    public class PermissionDao : BaseEFDao<T_Permission>, IPermissionDao
    {
        public IEnumerable<PermissionViewModel> GetPermission(int roleId)
        {
            using (DBEntity db = new DBEntity())
            {
                return (from p in db.T_Permission
                        where p.RoleId == roleId
                        select new PermissionViewModel
                        {
                            ControllerID = p.ControllerId,
                            ActionID = p.ActionId
                        }).ToList();
            }
        }

        public void ClearPermission(int roleId)
        {
            using (DBEntity db = new DBEntity())
            {
                foreach (T_Permission permission in db.T_Permission)
                {
                    if (permission.RoleId == roleId)
                    {
                        db.T_Permission.DeleteObject(permission);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
