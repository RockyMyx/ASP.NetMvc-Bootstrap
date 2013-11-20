using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using System.Data;

namespace MvcBootstrap.DAO
{
    public class RoleDao : BaseEFDao<Role>, IRoleDao
    {
    }
}
