using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.Service;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Controllers
{
    public class UserController : ManageController
    {
        UserService userService = new UserService();
        UserRoleService urService = new UserRoleService();

        public UserController()
        {
            base.cacheAllKey = "AllUsers";
            base.cacheSearchKey = "SearchUsers";
        }

        protected override int DataCount
        {
            get { return userService.GetEntitiesCount(); }
        }

        public override ActionResult Index()
        {
            Session.Remove(cacheAllKey);
            Session.Remove(cacheSearchKey);
            ViewData["RoleId"] = userService.GetRoleSelect();
            var result = userService.GetPagingUserView(base.PageSize);
            return View(result);
        }

        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IEnumerable<UserViewModel> entities = (IEnumerable<UserViewModel>)Session[cacheSearchKey] ??
                                                  userService.GetAllUserView();
            IEnumerable<UserViewModel> result = userService.GetSearchPagingUserView(entities, index, base.PageSize);
            return PartialView("_UserGrid", result);
        }

        public override void Create(FormCollection formInfo)
        {
            User user = userService.GetUserInfo(formInfo);
            userService.Create(user);
            UserRole ur = userService.GetNewUserRoleInfo(formInfo);
            urService.Create(ur);
        }

        public override void Delete(List<int> ids)
        {
            userService.Delete(ids);
            urService.Delete(ids);
        }

        public override void Update(FormCollection formInfo)
        {
            User user = userService.GetUserInfo(formInfo);
            userService.Update(user);
            UserRole ur = userService.GetEditUserRoleInfo(formInfo);
            urService.Update(ur);
        }

        public ActionResult Get(int id)
        {
            UserViewModel model = userService.GetUserViewModel(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<UserViewModel> filterEntities = userService.GetAllUserView()
                                                        .Where(m => m.Name.Contains(name));
            Session[cacheSearchKey] = filterEntities;
            if (filterEntities.Count() == 0) return new EmptyResult();
            return PartialView("_UserGrid", filterEntities);
        }

        public ActionResult GetResourceTreeNodes()
        {
            //ToTest
            //int userId = Convert.ToInt32(Session["UserId"]);
            int userId = 1;

            UserNodeService unService = new UserNodeService();
            List<TreeViewModel> treeNodes = unService.GetCategoryNodes(userId);
            return Json(treeNodes, JsonRequestBehavior.AllowGet);
        }
    }
}
