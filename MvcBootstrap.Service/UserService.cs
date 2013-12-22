﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.IService;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.DAO;

namespace MvcBootstrap.Service
{
    public class UserService : BaseService<User, IUserDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserDao();
        }

        public IEnumerable<UserBrowseViewModel> GetUserBrowse(int roleId)
        {
            return base.dao.GetUserBrowse(roleId);
        }
    }
}
