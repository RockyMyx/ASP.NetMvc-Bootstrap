using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBootstrap.EFModel;
using MvcBootstrap.Service;

namespace MvcBootstrap.ViewModels
{
    public class RoleEditViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string IsEnable { get; set; }

        public static void ToSaveEntity(RoleEditViewModel viewModel)
        {
            using (DBEntity db = new DBEntity())
            {
                RoleService service = new RoleService();
                Role role = service.GetEntity(m => m.ID == viewModel.ID);
                role.Name = viewModel.Name;
                role.Remark = viewModel.Remark;
                role.IsEnable = int.Parse(viewModel.IsEnable) == 1 ? true : false;
                db.SaveChanges();
            }
        }
    }
}