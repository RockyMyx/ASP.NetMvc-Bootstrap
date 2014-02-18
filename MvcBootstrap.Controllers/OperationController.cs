using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.Service;
using System.Web.Mvc;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.Controllers
{
    public class OperationController : ManageController
    {
        OperationService opService = new OperationService();

        public OperationController()
        {
            base.cacheAllKey = "AllOperations";
            base.cacheSearchKey = "SearchOperations";
        }

        protected override int DataCount
        {
            get { return opService.GetEntitiesCount(); }
        }

        public override ActionResult Index()
        {
            Session.Remove(cacheAllKey);
            Session.Remove(cacheSearchKey);
            var result = opService.GetPagingInfo(base.PageSize);
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IEnumerable<Operation> entities = (IEnumerable<Operation>)Session[cacheSearchKey] ??
                                              opService.GetAll();
            IEnumerable<Operation> result = opService.GetSearchPagingInfo(entities, index, base.PageSize);
            return PartialView("_OperationGrid", result);
        }

        public override void Create(FormCollection formInfo)
        {
            Operation module = opService.GetOperationInfo(formInfo);
            opService.Create(module);
        }

        public ActionResult Modify(Operation operation)
        {
            opService.Update(operation);
            return MyJson(operation);
        }

        public override void Delete(List<int> ids)
        {
            opService.Delete(ids);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<Operation> filterEntities = opService.GetAll().Where(m => m.Name.Contains(name));
            Session[cacheSearchKey] = filterEntities;
            if (filterEntities.Count() == 0) return new EmptyResult();
            return PartialView("_OperationGrid", filterEntities);
        }
    }
}
