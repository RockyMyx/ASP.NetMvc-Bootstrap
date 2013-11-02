using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View(result);
        }

        public override ActionResult Index(int? pageIndex)
        {
            throw new NotImplementedException();
        }

        public override ActionResult Update(FormCollection formInfo)
        {
            throw new NotImplementedException();
        }

        public override ActionResult Delete(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public override ActionResult Create(FormCollection formInfo)
        {
            throw new NotImplementedException();
        }

        public override ActionResult Search(string name)
        {
            throw new NotImplementedException();
        }
    }
}
