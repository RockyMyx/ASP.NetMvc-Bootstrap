using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class RoleService : BaseService<Role, IRoleDao>
    {
        public RoleService()
        {
            base.dao = new RoleDao();
        }
    }
}