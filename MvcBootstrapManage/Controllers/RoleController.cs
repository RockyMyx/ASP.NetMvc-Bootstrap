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
        protected override int TotalCount
        {
            get { return db.Role.Count(); }
        }

        public override ActionResult Index()
        {
            var result = db.Role.Take(base.PageSize).ToList();
            ViewData["ParentModule"] = db.Module.Where(m => m.ParentId == null).ToList();
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IList<Role> result = db.Role.OrderBy(r => r.ID).Skip((index - 1) * 3).Take(base.PageSize).ToList();
            return PartialView("_RoleGrid", result);
        }

        [HttpPost]
        public ActionResult Modify(RoleEditViewModel viewModel)
        {
            Role role = db.Role.Where(m => m.ID == viewModel.ID).Single();
            role.Name = viewModel.Name;
            role.Remark = viewModel.Remark;
            role.IsEnable = int.Parse(viewModel.IsEnable) == 1 ? true : false;
            db.SaveChanges();
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult Update(FormCollection formInfo)
        {
            throw new NotImplementedException();
        }

        public override ActionResult Delete(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    Role role = db.Role.Where(m => m.ID == id).FirstOrDefault();
                    if (role != null)
                    {
                        db.DeleteObject(role);
                        db.SaveChanges();
                    }
                }
            }
            catch (OptimisticConcurrencyException)
            {
                db.AcceptAllChanges();
            }

            return new EmptyResult();
        }

        public override ActionResult Create(FormCollection formInfo)
        {
            Role role = GetRoleFromForm(formInfo);
            role.CreateDate = DateTime.Now;
            //role.Creator = Session["RealName"].ToString();
            db.Role.AddObject(role);
            db.SaveChanges();
            return new EmptyResult();
        }

        public override ActionResult Search(string name)
        {
            IList<Role> result = db.Role.Where(m => m.Name.Contains(name)).ToList();
            if (result.Count == 0)
            {
                return new EmptyResult();
            }
            return PartialView("_RoleGrid", result);
        }

        [HttpPost]
        public ActionResult SetPermission(int id, FormCollection formInfo)
        {
            //Reset
            string sql = "DELETE FROM permission WHERE roleID = " + id;
            db.ExecuteStoreCommand(sql);

            int controllerId;
            int actionId;
            List<int> parentIds = new List<int>();
            foreach (string item in formInfo.AllKeys)
            {
                controllerId = Convert.ToInt32(item.Split('-')[0]);
                actionId = Convert.ToInt32(item.Split('-')[1]);
                db.Permission.AddObject(new Permission() { RoleID = id, ControllerID = controllerId, ActionID = actionId });

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
                db.Permission.AddObject(new Permission() { RoleID = id, ControllerID = parentId, ActionID = 1 });
            }

            db.SaveChanges();
            return new EmptyResult();
        }

        private Role GetRoleFromForm(FormCollection formInfo)
        {
            Role role = new Role();
            role.Name = formInfo["Name"].ToString();
            role.Remark = formInfo["Remark"].ToString();
            role.IsEnable = formInfo["IsEnable"] == null ? true : string.Compare(formInfo["IsEnable"], "1") == 0;
            return role;
        }
    }
}
