using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.DAO;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Service
{
    public class PermissionService : BaseService<Permission, IPermissionDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new PermissionDao();
        }

        public IEnumerable<PermissionViewModel> GetPermission(int roleId)
        {
            return base.dao.GetPermission(roleId);
        }

        public void ClearPermission(int roleId)
        {
            base.dao.ClearPermission(roleId);
        }
    }
}
