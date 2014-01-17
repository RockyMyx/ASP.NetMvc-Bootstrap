﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcBootstrap.Controllers
{
    [OperationAuthorize]
    public abstract class ManageController : BaseController
    {
        protected ManageController()
        {
            ViewBag.dataCount = this.DataCount;
            ViewBag.pageSize = this.PageSize;
            RemoveCache();
        }

        protected int PageSize { get { return 3; } }
        protected abstract int DataCount { get; }

        public virtual void RemoveCache() { }

        public abstract ActionResult Index();
        [HttpPost]
        public abstract ActionResult Index(int? pageIndex);
        [HttpPost]
        public abstract void Create(FormCollection formInfo);
        [HttpPost]
        public abstract void Delete(List<int> ids);
        [HttpPost]
        public abstract ActionResult Search(string name);

        [HttpPost]
        public virtual ActionResult AdvanceSearch(FormCollection searchFormInfo) { return null; }

        //编辑时使用window方式时实现Update方法，否则实现Modify方法
        [HttpPost]
        public virtual void Update(FormCollection formInfo) { }
    }
}