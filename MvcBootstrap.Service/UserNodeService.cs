using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.ViewModels;
using System;
using System.Text;

namespace MvcBootstrap.Service
{
    public class UserNodeService : BaseService<UserNode, IUserNodeDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserNodeDao();
        }
    }
}
