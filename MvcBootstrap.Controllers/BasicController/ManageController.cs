using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBootstrap.EFModel;
using System.Web.Mvc;
using MvcBootstrap.ViewModels;
using System.Data.Objects.DataClasses;

namespace MvcBootstrap.Controllers
{
    [OperationAuthorize]
    public abstract class ManageController : BaseController
    {
        public ManageController()
        {
            ViewBag.dataCount = this.DataCount;
            ViewBag.pageSize = this.PageSize;
        }

        protected int PageSize { get { return 3; } }
        protected abstract int DataCount { get; }

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