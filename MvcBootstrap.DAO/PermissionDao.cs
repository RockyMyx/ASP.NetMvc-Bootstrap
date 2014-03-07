using System.Collections.Generic;
using System.Linq;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.DAO
{
    public class PermissionDao : BaseEFDao<Permission>, IPermissionDao
    {
        public IEnumerable<PermissionViewModel> GetPermission(int roleId)
        {
            using (DBEntity db = new DBEntity())
            {
                return (from p in db.Permission
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
                foreach (Permission permission in db.Permission)
                {
                    if (permission.RoleID == roleId)
                    {
                        db.Permission.DeleteObject(permission);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
