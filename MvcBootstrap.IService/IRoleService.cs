using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.IService
{
    public interface IRoleService : IBaseService<Role, IRoleDao>
    {
    }
}
