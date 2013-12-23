using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using MvcBootstrap.ViewModels;
using System.Data;
using MvcBootstrap.Service;

namespace MvcBootstrap.Controllers
{
    public class RoleController : ManageController
    {
        RoleService service = new RoleService();

        protected override int DataCount
        {
            get { return service.GetEntitiesCount(); }
        }

        public override ActionResult Index()
        {
            var result = service.GetPagingInfo(base.PageSize);
            ModuleService moduleService = new ModuleService();
            IEnumerable<Module> modules = moduleService.GetAll().ToList();
            IList<Module> parent = new List<Module>();
            foreach (Module module in modules)
            {
                if (module.ParentId == null)
                {
                    parent.Add(module);
                }
            }

            ViewData["ParentModule"] = parent;
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IEnumerable<Role> result = service.GetPagingInfo(r => r.ID, index, base.PageSize);
            return PartialView("_RoleGrid", result);
        }

        public ActionResult Modify(Role role)
        {
            service.Update(role);
            return Json(role);
        }

        public override void Delete(List<int> ids)
        {
            service.Delete(ids);
        }

        public override void Create(FormCollection formInfo)
        {
            Role role = FormHelper.GetRoleInfo(formInfo);
            role.CreateDate = DateTime.Now;
            //ToTest
            //role.CreateUserID = Convert.ToInt32(Session["UserID"]);

            service.Create(role);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<Role> result = service.GetEntities(m => m.Name.Contains(name));
            if (result.Count() == 0) return new EmptyResult();
            return PartialView("_RoleGrid", result);
        }

        public ActionResult GetPermission(int id)
        {
            PermissionService service = new PermissionService();
            IEnumerable<PermissionViewModel> permissions = service.GetPermission(id);
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

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [HttpPost]
        public void SetPermission(int id, FormCollection formInfo)
        {
            int controllerId;
            int actionId;

            //ToTest
            //int modifyUserId = Convert.ToInt32(Session["UserId"]);
            int modifyUserId = 1;

            List<int> parentIds = new List<int>();
            PermissionService service = new PermissionService();
            service.ClearPermission(id);

            foreach (string item in formInfo.AllKeys)
            {
                controllerId = Convert.ToInt32(item.Split('-')[0]);
                actionId = Convert.ToInt32(item.Split('-')[1]);

                service.Create(new Permission() 
                                { 
                                    RoleID = id, 
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
                service.Create(new Permission()
                {
                    RoleID = id,
                    ControllerID = parentId,
                    ActionID = 1,
                    ModifyUserID = modifyUserId,
                    ModifyDate = DateTime.Now
                });
            }
        }
    }
}
