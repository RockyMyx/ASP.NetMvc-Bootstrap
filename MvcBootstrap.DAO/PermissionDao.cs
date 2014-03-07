using System.Collections.Generic;
using System.Linq;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;
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
                        where p.RoleID == roleId
                        select new PermissionViewModel
                        {
                            ControllerID = p.ControllerID,
                            ActionID = p.ActionID
                        }).ToList();
            }
        }

        public void ClearPermission(int roleId)
        {
            using (DBEntity db = new DBEntity())
            {
                foreach (T_Permission permission in db.T_Permission)
                {
                    if (permission.RoleID == roleId)
                    {
                        db.T_Permission.DeleteObject(permission);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
