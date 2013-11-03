using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBootstrapManage.Models;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;

namespace MvcBootstrapManage.Controllers
{
    public class ModuleController : ManageController
    {
        protected override int TotalCount
        {
            get { return db.Module.Count(); }
        }

        public override ActionResult Index()
        {
            List<Module> modules = db.Module.ToList();
            List<SelectListItem> moduleList = new List<SelectListItem>();
            int isParent;
            moduleList.Add(new SelectListItem { Text = "请选择", Value = "0" });
            for (int i = 0; i < modules.Count; i++)
            {
                if (!int.TryParse(modules[i].ParentId.ToString(), out isParent))
                {
                    moduleList.Add(new SelectListItem { Text = modules[i].Name, Value = modules[i].ID.ToString() });
                }
            }
            ViewData["ParentId"] = moduleList;
            var result = db.GetModuleTree().Take(base.PageSize).ToList();
            return View(result);
        }

        [HttpPost]
        public override ActionResult Index(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            IList<Module> result = db.GetModuleTree().Skip((index - 1) * 3).Take(base.PageSize).ToList();
            return PartialView("_ModuleGrid", result);
        }

        [HttpPost]
        public override ActionResult Update(FormCollection formInfo)
        {
            int id = Convert.ToInt32(formInfo["ID"]);
            Module module = GetModuleFromForm(formInfo);
            Module oldModule = db.Module.Where(m => m.ID == id).Single();
            oldModule.ID = id;
            oldModule.Name = module.Name;
            oldModule.Controller = module.Controller;
            oldModule.IsEnable = module.IsEnable;
            oldModule.Operations = module.Operations;
            db.SaveChanges();
            return new EmptyResult();
        }

        [HttpPost]
        public override ActionResult Delete(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    Module module = db.Module.Where(m => m.ID == id).Single();
                    db.DeleteObject(module);
                    db.SaveChanges();
                }
            }
            catch (OptimisticConcurrencyException)
            {
                db.AcceptAllChanges();
            }

            return new EmptyResult();
        }

        [HttpPost]
        public override ActionResult Create(FormCollection formInfo)
        {
            Module module = GetModuleFromForm(formInfo);
            module.CreateDate = DateTime.Now;
            //module.Creator = Session["RealName"].ToString();
            db.Module.AddObject(module);
            db.SaveChanges();
            return new EmptyResult();
        }

        public override ActionResult Search(string name)
        {
            IList<Module> result = db.GetModuleTree().Where(m => m.Name.Contains(name)).ToList();
            if (result.Count == 0)
            {
                return new EmptyResult();
            }
            return PartialView("_ModuleGrid", result);
        }

        [HttpPost]
        public ActionResult AdvanceSearch(FormCollection searchFormInfo)
        {
            Module module = GetModuleFromForm(searchFormInfo);
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

        private Module GetModuleFromForm(FormCollection formInfo)
        {
            Module module = new Module();
            module.Name = formInfo["Name"].ToString();
            module.Code = formInfo["Code"].ToString();
            module.Controller = formInfo["Controller"].ToString();
            module.IsEnable = formInfo["IsEnable"] == null ? true : string.Compare(formInfo["IsEnable"], "1") == 0;
            int parentId = Convert.ToInt32(formInfo["ParentId"]);
            if (parentId != 0)
            {
                module.ParentId = parentId;
            }

            List<string> operations = formInfo.AllKeys.Where(k => k.Contains("op")).ToList();
            if (operations.Count > 0)
            {
                StringBuilder strOperation = new StringBuilder();
                foreach (string operation in operations)
                {
                    strOperation.Append(operation.Replace("op", "") + ",");
                }
                module.Operations = strOperation.Remove(strOperation.Length - 1, 1).ToString();
            }

            return module;
        }
    }
}