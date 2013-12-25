using System.Collections.Generic;
using MvcBootstrap.EFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.IDAO
{
    public interface IPermissionDao : IBaseDao<Permission>
    {
        IEnumerable<PermissionViewModel> GetPermission(int roleId);
        void ClearPermission(int roleId);
    }
}
