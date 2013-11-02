using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBootstrapManage.Models;
using System.Web.Mvc;
using MvcBootstrapManage.ViewModel;

namespace MvcBootstrapManage.Controllers
{
    [OperationAuthorize]
    public abstract class ManageController : BaseController
    {
        public ManageController()
        {
            ViewBag.TotalCount = this.TotalCount;
            ViewBag.PageSize = this.PageSize;
            ViewBag.HideSearch = this.PageSize - 1;
        }

        protected DBEntity db = new DBEntity();
        protected int PageSize = 3;
        protected abstract int TotalCount { get; }

        public abstract ActionResult Index();
        [HttpPost]
        public abstract ActionResult Index(int? pageIndex);
        [HttpPost]
        public abstract ActionResult Delete(List<int> ids);
        //编辑时使用window方式时实现，否则实现Modify方法
        [HttpPost]
        public abstract ActionResult Update(FormCollection formInfo);
        [HttpPost]
        public abstract ActionResult Create(FormCollection formInfo);
        [HttpPost]
        public abstract ActionResult Search(string name);
    }
}