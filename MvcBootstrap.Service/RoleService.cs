using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.Service
{
    public class RoleService : BaseService<Role, IRoleDao>
    {
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