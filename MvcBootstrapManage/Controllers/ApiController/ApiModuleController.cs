using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using MvcBootstrapManage.Models;

namespace MvcBootstrapManage.Controllers
{
    public class ApiModuleController : ApiController
    {
        public Module GetModuleById(int id)
        {
            using (DBEntity db = new DBEntity())
            {
                return db.Module.Where(m => m.ID == id).Single();
            }
        }
    }
}
