using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using MvcBootstrap.Service;

namespace MvcBootstrap.Controllers
{
    public class RoleController : ManageController
    {
        RoleService roleService = new RoleService();

        protected override int DataCount
        {
            get { return roleService.GetEntitiesCount(); }
        }

        public override ActionResult Index()
        {
            roleService.RemoveSearchCache();
            var result = roleService.GetPagingInfo(base.PageSize);
            ModuleService moduleService = new ModuleService();
            ViewData["ParentModule"] = moduleService.GetParentModules();
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IEnumerable<Role> result = roleService.GetSearchPagingInfo(
                                       roleService.GetSearchCache(), index, base.PageSize);
            return PartialView("_RoleGrid", result);
        }

        public ActionResult Modify(Role role)
        {
            roleService.Update(role);
            return Json(role);
        }

        public override void Delete(List<int> ids)
        {
            roleService.Delete(ids);
        }

        public override void Create(FormCollection formInfo)
        {
            Role role = roleService.GetRoleInfo(formInfo);
            role.CreateDate = DateTime.Now;
            //ToTest
            //role.CreateUserID = Convert.ToInt32(Session["UserID"]);
            roleService.Create(role);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<Role> result = roleService.GetSearchCache(
                                       roleService.GetEntityCache().Where(m => m.Name.Contains(name)),
                                       true);
            if (result.Count() == 0) return new EmptyResult();
            return PartialView("_RoleGrid", result);
        }

        public ActionResult GetPermission(int id)
        {
            PermissionService permissionService = new PermissionService();
            Dictionary<string, string> permissionList = permissionService.BuildPermission(id);
            return Json(permissionList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SetPermission(int id, FormCollection formInfo)
        {
            //ToTest
            //int modifyUserId = Convert.ToInt32(Session["UserId"]);
            int modifyUserId = 1;
            PermissionService permissionService = new PermissionService();
            permissionService.ClearPermission(id);
            permissionService.CreatePermission(id, formInfo.AllKeys, modifyUserId);
        }
    }
}
