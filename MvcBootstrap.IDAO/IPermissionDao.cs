using System.Collections.Generic;
using MvcBootstrap.MysqlEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.IDAO
{
    public interface IPermissionDao : IBaseDao<permission>
    {
        IEnumerable<PermissionViewModel> GetPermission(int roleId);
        void ClearPermission(int roleId);
    }
}
