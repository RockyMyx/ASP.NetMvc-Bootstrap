using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBootstrapManage.ViewModel
{
    public class RoleEditViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string IsEnable { get; set; }
    }
}