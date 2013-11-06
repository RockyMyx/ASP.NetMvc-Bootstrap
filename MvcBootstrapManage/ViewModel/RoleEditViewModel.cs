using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBootstrapManage.Models;

namespace MvcBootstrapManage.ViewModel
{
    public class RoleEditViewModel : BaseViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string IsEnable { get; set; }

        public static void ToSaveEntity(RoleEditViewModel viewModel)
        {
            using (DBEntity db = new DBEntity())
            {
                Role role = db.Role.GetEntity(m => m.ID == viewModel.ID);
                role.Name = viewModel.Name;
                role.Remark = viewModel.Remark;
                role.IsEnable = int.Parse(viewModel.IsEnable) == 1 ? true : false;
                db.SaveChanges();
            }
        }
    }
}