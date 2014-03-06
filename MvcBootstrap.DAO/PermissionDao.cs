using System.Collections.Generic;
using System.Linq;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.DAO
{
    public class PermissionDao : BaseEFDao<permission>, IPermissionDao
    {
        public IEnumerable<PermissionViewModel> GetPermission(int roleId)
        {
            using (DBEntity db = new DBEntity())
            {
                return (from p in db.permission
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
                foreach(permission permission in db.permission)
                {
                    if (permission.RoleID == roleId)
                    {
                        db.permission.DeleteObject(permission);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
