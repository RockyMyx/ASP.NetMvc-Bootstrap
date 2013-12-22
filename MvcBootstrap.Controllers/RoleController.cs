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
            using (DBEntity db = new DBEntity())
            {
                var permissions = db.Permission.Where(p => p.RoleID == id)
                                    .Select(p => new { p.ControllerID, p.ActionID })
                                    .AsEnumerable();
                if (permissions.Count() == 0)
                {
                    return null;
                }
                else
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
            }
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
            foreach (string item in formInfo.AllKeys)
            {
                controllerId = Convert.ToInt32(item.Split('-')[0]);
                actionId = Convert.ToInt32(item.Split('-')[1]);

                if (!isPermissionExist(id, controllerId, actionId))
                {
                    db.Permission.AddObject(new Permission() { RoleID = id, ControllerID = controllerId, ActionID = actionId, ModifyUserID = modifyUserId, ModifyDate = DateTime.Now });
                }

                //添加父节点
                int parentId = Convert.ToInt32(db.Module.Where(m => m.ID == controllerId)
                                                        .Select(m => m.ParentId)
                                                        .Single());
                if (!parentIds.Contains(parentId))
                {
                    parentIds.Add(parentId);
                }
            }

            foreach (int parentId in parentIds)
            {
                if (!isPermissionExist(id, parentId, 1))
                {
                    db.Permission.AddObject(new Permission() { RoleID = id, ControllerID = parentId, ActionID = 1, ModifyUserID = modifyUserId, ModifyDate = DateTime.Now });
                }
            }

            db.SaveChanges();
        }

        private bool isPermissionExist(int id, int controllerId, int actionId)
        {
            return Convert.ToBoolean(db.Permission.Where(p => p.ID == id &&
                                       p.ControllerID == controllerId &&
                                       p.ActionID == actionId).Count());
        }
    }
}
