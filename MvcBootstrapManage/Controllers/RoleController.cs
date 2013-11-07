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
            //var result = db.Role.Take(base.PageSize).ToList();
            //ViewData["ParentModule"] = db.Module.Where(m => m.ParentId == null).ToList();

            var result = db.Role.GetPagingInfo(base.PageSize);
            ViewData["ParentModule"] = db.Module.GetEntities(m => m.ParentId == null);
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            //IList<Role> result = db.Role.OrderBy(r => r.ID).Skip((index - 1) * base.PageSize).Take(base.PageSize).ToList();
            IEnumerable<Role> result = db.Role.GetPagingInfo(r => r.ID, index, base.PageSize);
            return PartialView("_RoleGrid", result);
        }

        public ActionResult Modify(RoleEditViewModel viewModel)
        {
            //Role role = db.Role.Where(m => m.ID == viewModel.ID).Single();
            //role.Name = viewModel.Name;
            //role.Remark = viewModel.Remark;
            //role.IsEnable = int.Parse(viewModel.IsEnable) == 1 ? true : false;

            //Role role = RoleEditViewModel.ToEntity(viewModel);
            //db.SaveChanges();
            RoleEditViewModel.ToSaveEntity(viewModel);
            return Json(viewModel);
        }

        public override void Delete(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    Role role = db.Role.GetEntity(r => r.ID == id);
                    db.DeleteObject(role);
                    db.SaveChanges();
                }
            }
            catch (OptimisticConcurrencyException)
            {
                db.AcceptAllChanges();
            }
        }

        public override void Create(FormCollection formInfo)
        {
            Role role = FormHelper.GetRoleInfo(formInfo);
            role.CreateDate = DateTime.Now;
            //role.CreateUserID = Convert.ToInt32(Session["UserID"]);
            db.Role.AddObject(role);
            db.SaveChanges();
        }

        public override ActionResult Search(string name)
        {
            IEnumerable<Role> result = db.Role.Where(m => m.Name.Contains(name));
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
            //int createUserId = Convert.ToInt32(Session["UserId"]);
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
