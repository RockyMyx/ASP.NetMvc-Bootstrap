using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class RoleService : BaseService<Role, IRoleDao>
    {
        public RoleService()
        {
            base.cacheAllKey = "AllRoles";
        }

        protected override void SetCurrentDao()
        {
            base.dao = new RoleDao();
        }

        public Role GetRoleInfo(FormCollection formInfo)
        {
            Role role = new Role
            {
                Name = formInfo["Name"].ObjToStr(),
                Remark = formInfo["Remark"].ObjToStr(),
                IsEnable = formInfo["IsEnable"] == null || string.Compare(formInfo["IsEnable"], "1") == 0
            };

            return role;
        }
    }
}