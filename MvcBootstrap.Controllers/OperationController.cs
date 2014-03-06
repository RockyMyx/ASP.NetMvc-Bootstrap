using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBootstrap.MysqlEFModel;
using MvcBootstrap.Service;

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
            IEnumerable<operation> entities = (IEnumerable<operation>)Session[cacheSearchKey] ??
                                              opService.GetAll();
            IEnumerable<operation> result = opService.GetSearchPagingInfo(entities, index, base.PageSize);
            return PartialView("_OperationGrid", result);
        }

        public override void Create(FormCollection formInfo)
        {
            operation module = opService.GetOperationInfo(formInfo);
            opService.Create(module);
        }

        public ActionResult Modify(operation operation)
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
            IEnumerable<operation> filterEntities = opService.GetAll().Where(m => m.Name.Contains(name));
            Session[cacheSearchKey] = filterEntities;
            if (filterEntities.Count() == 0) return new EmptyResult();
            return PartialView("_OperationGrid", filterEntities);
        }
    }
}
