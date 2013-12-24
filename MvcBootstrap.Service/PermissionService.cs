using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.DAO;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Service
{
    public class PermissionService : BaseService<Permission, IPermissionDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new PermissionDao();
        }

        public IEnumerable<PermissionViewModel> GetPermission(int roleId)
        {
            return base.dao.GetPermission(roleId);
        }

        public void ClearPermission(int roleId)
        {
            base.dao.ClearPermission(roleId);
        }

        public Dictionary<string, string> BuildPermission(int roleId)
        {
            IEnumerable<PermissionViewModel> permissions = this.GetPermission(roleId);
            if (permissions.Count() != 0)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                string controllerId;
                foreach (var permission in permissions)
                {
                    controllerId = permission.ControllerID.ToString();
                    if (!result.ContainsKey(controllerId))
                    {
                        result.Add(controllerId, permission.ActionID.ToString());
                    }
                    else
                    {
                        result[controllerId] += permission.ActionID.ToString();
                    }
                }

                return result;
            }

            return null;
        }

        public void CreatePermission(int roleId, IEnumerable<string> permissions, int modifyUserId)
        {
            int controllerId;
            int actionId;
            List<int> parentIds = new List<int>();
            foreach (string item in permissions)
            {
                controllerId = Convert.ToInt32(item.Split('-')[0]);
                actionId = Convert.ToInt32(item.Split('-')[1]);
                base.dao.Create(new Permission()
                {
                    RoleID = roleId,
                    ControllerID = controllerId,
                    ActionID = actionId,
                    ModifyUserID = modifyUserId,
                    ModifyDate = DateTime.Now
                });

                ModuleService moduleService = new ModuleService();
                //添加父节点
                int parentId = moduleService.GetModuleParentId(controllerId);
                if (!parentIds.Contains(parentId))
                {
                    parentIds.Add(parentId);
                }
            }

            foreach (int parentId in parentIds)
            {
                base.dao.Create(new Permission()
                {
                    RoleID = roleId,
                    ControllerID = parentId,
                    ActionID = 1,
                    ModifyUserID = modifyUserId,
                    ModifyDate = DateTime.Now
                });
            }
        }
    }
}
