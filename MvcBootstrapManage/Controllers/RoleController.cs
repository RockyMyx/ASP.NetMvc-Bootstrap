using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;
using MvcBootstrapManage.ViewModel;
using System.Data;

namespace MvcBootstrapManage.Controllers
{
    public class RoleController : ManageController
    {
        protected override int DataCount
        {
            get { return db.Role.Count(); }
        }

        public override ActionResult Index()
        {
            var result = db.Role.GetPagingInfo(base.PageSize);
            ViewData["ParentModule"] = db.Module.GetEntities(m => m.ParentId == null);
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IEnumerable<Role> result = db.Role.GetPagingInfo(r => r.ID, index, base.PageSize);
            return PartialView("_RoleGrid", result);
        }

        public ActionResult Modify(RoleEditViewModel viewModel)
        {
            RoleEditViewModel.ToSaveEntity(viewModel);
            return Json(viewModel);
        }

        public override void Delete(List<int> ids)
        {
            Role role = null;
            foreach (int id in ids)
            {
                role = db.Role.GetEntity(r => r.ID == id);
                db.DeleteObject(role);
                db.SaveChanges();
            }
        }

        public override void Create(FormCollection formInfo)
        {
            Role role = FormHelper.GetRoleInfo(formInfo);
            role.CreateDate = DateTime.Now;
            //ToTest
            //role.CreateUserID = Convert.ToInt32(Session["UserID"]);
            db.Role.AddObject(role);
            db.SaveChanges();
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IList<Role> result = db.Role.Where(m => m.Name.Contains(name)).ToList();
            if (result.Count() == 0)
            {
                return new EmptyResult();
            }

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
            int modifyUserId = 2;

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
