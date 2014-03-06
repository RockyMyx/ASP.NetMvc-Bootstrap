using System.Web.Mvc;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
using MvcBootstrap.MysqlEFModel;

namespace MvcBootstrap.Service
{
    public class RoleService : BaseService<role, IRoleDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new RoleDao();
        }

        public role GetRoleInfo(FormCollection formInfo)
        {
            role role = new role
            {
                Name = formInfo["Name"].ObjToStr(),
                Remark = formInfo["Remark"].ObjToStr(),
                IsEnable = formInfo["IsEnable"] == null || string.Compare(formInfo["IsEnable"], "1") == 0
            };

            return role;
        }
    }
}