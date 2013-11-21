using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrap.EFModel;
using System.Text;
using System.Data;
using System.Data.Objects;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Controllers
{
    public class ModuleController : ManageController
    {
        IModuleDao dao = null;
        public ModuleController()
        {
            dao = new ModuleDao();
        }

        protected override int DataCount
        {
            get { return db.Module.Count(); }
        }

        public override ActionResult Index()
        {
            ViewData["ParentId"] = dao.GetModuleSelect();
            var result = dao.GetPagingInfo(base.PageSize);
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            ViewData["ParentId"] = dao.GetModuleSelect();
            int index = pageIndex ?? 1;
            IEnumerable<Module> result = dao.GetPagingInfo(index, base.PageSize);
            return PartialView("_ModuleGrid", result);
        }

        [HttpPost]
        public override void Update(FormCollection formInfo)
        {
            Module module = FormHelper.GetModuleInfo(formInfo);
            dao.Update(module);
        }

        [HttpPost]
        public override void Delete(List<int> ids)
        {
            dao.Delete(ids);
        }

        [HttpPost]
        public override void Create(FormCollection formInfo)
        {
            Module module = FormHelper.GetModuleInfo(formInfo);
            dao.Create(module);
        }

        public override ActionResult Search(string name)
        {
            name = name.Trim();
            IEnumerable<Module> result = dao.GetEntities(m => m.Name.Contains(name));
            if (result.Count() == 0) return new EmptyResult();
            return PartialView("_ModuleGrid", result);
        }

        public ActionResult Get(int id)
        {
            Module module = dao.GetEntity(m => m.ID == id);
            return Json(module, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public override ActionResult AdvanceSearch(FormCollection searchFormInfo)
        {
            try
            {
                Module module = FormHelper.GetModuleInfo(searchFormInfo);
                IQueryable<Module> search = db.GetModuleTree().AsQueryable();
                if (!string.IsNullOrEmpty(module.Name))
                {
                    search = search.Where(m => m.Name.Contains(module.Name));
                }
                if (!string.IsNullOrEmpty(module.Code))
                {
                    search = search.Where(m => m.Name.Contains(module.Code));
                }
                if (!string.IsNullOrEmpty(module.Controller))
                {
                    search = search.Where(m => m.Name.Contains(module.Controller));
                }
                if (module.ParentId != null)
                {
                    search = search.Where(m => m.ParentId == module.ParentId);
                }

                List<Module> result = search.Where(m => m.IsEnable == module.IsEnable).ToList();
                if (result.Count == 0)
                {
                    return new EmptyResult();
                }

                return PartialView("_ModuleGrid", result);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}