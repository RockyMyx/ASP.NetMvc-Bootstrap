using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcBootstrap.ViewModels
{
    public class UserLoginViewModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RealName { get; set; }
        public string UserName { get; set; }
    }
}
